using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Planet; 
    public float radius;
    public float orbitRadius;
    public float surfaceGravity;
    public Shader shader;
    public Color newColor;
    public CelestialBody body;
    public string bodyName;
    GameObject currentEntity;
    
    /*All the Read methods are to get the values from the Create Planet UI*/
    public void ReadRadius(string radiuss)
    {
        radius = float.Parse(radiuss);
    }

    public void ReadOrbitRadius(string orbitRadiuss)
    {
        orbitRadius = float.Parse(orbitRadiuss);
    }
    
    public void ReadSurfaceGravity(string surfaceGravitys)
    {
        surfaceGravity = float.Parse(surfaceGravitys);
    }

    public void ReadName(string name)
    {
        bodyName = name;
    }

    /*Get's the input from the Hexadecimal input field*/
    public void OnMaterialEditEnd(string input)
    {
        if (IsHex(input))
        {
            newColor = Hetx2RGB(input);
        }
    }

    //Check if if our input is Valid for Hex Colours.
    public bool IsHex(string hex)
    {
        char[] chars = hex.ToCharArray();

        bool isHex;
        foreach (var c in chars)
        {
            isHex = ((c >= '0' && c <= '9') ||
                     (c >= 'a' && c <= 'f') ||
                     (c >= 'A' && c <= 'F'));

            if (!isHex)
                return false;
        }
        return true;

    }

    public Color Hetx2RGB(string hex)
    {

        char[] values = hex.ToCharArray();
        Color newColor = Color.white;

        //Make sure we dont have any alpha values
        if (hex.Length != 6)
        {
            return newColor;
        }

        var hexRed = int.Parse(hex[0].ToString() + hex[1].ToString(),
        System.Globalization.NumberStyles.HexNumber);

        var hexGreen = int.Parse(hex[2].ToString() + hex[3].ToString(),
        System.Globalization.NumberStyles.HexNumber);

        var hexBlue = int.Parse(hex[4].ToString() + hex[5].ToString(),
        System.Globalization.NumberStyles.HexNumber);

        newColor = new Color(hexRed / 255f, hexGreen / 255f, hexBlue / 255f);

        return newColor;

    }

    /*Creates the new Planet*/
    public void SpawnEntities()
    {
        Vector3 radiusOrbit = new Vector3(-orbitRadius, 0, 0);
        if(body == null || body.name == "Sun")
        {
            currentEntity = Instantiate(Planet, radiusOrbit, Quaternion.identity, this.transform);
        }
        else
        {
            currentEntity = Instantiate(Planet, radiusOrbit, Quaternion.identity, body.transform);
        }

        /*Sets the color of the planet*/
        GameObject mesh = currentEntity.transform.Find("Mesh Holder").gameObject;
        mesh.transform.localScale = Vector3.one * radius;
        mesh.GetComponent<TerrainGenerator>().material = new Material(shader);
        mesh.GetComponent<TerrainGenerator>().material.SetColor("_Color", newColor);
        
        /*Set's the initial velocity based on the distance and mass of the Sun and a scaling factor*/
        GameObject go = GameObject.Find("Sun");
        CelestialBody massBody = go.GetComponent<CelestialBody>();
        Vector3 initialVelocity = new Vector3(0, 1, 0) * (float)(Mathf.Sqrt((Universe.gravitationalConstant * massBody.mass / (orbitRadius + body.orbitRadius)) * 1.5f));
        
        /*Passes the values of the new Planet to the CelestialBody script and makes sure that the new Planet is in the array to update the velocity*/
        CelestialBody createdBody = currentEntity.GetComponent<CelestialBody>();
        createdBody.CreateBody(radius, orbitRadius, surfaceGravity, initialVelocity, body, bodyName);
        TerrainGenerator createMesh = mesh.GetComponent<TerrainGenerator>();
        createMesh.CreateMesh(true);
        NBodySimulation newBody = FindObjectOfType<NBodySimulation>();
        newBody.NewBody();
    }
}
