using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Planet; 
    public float radius;
    public float orbitRadius;
    public float surfaceGravity;
    public Color color;
    public Shader shader;
    public CelestialBody body;
    public string bodyName;
    GameObject currentEntity;
    void Update()
    {
        if (Input.GetKeyDown("h"))
        {
            SpawnEntities();
        }
    }

    void SpawnEntities()
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
        
        Debug.Log("Vector3 = " + radiusOrbit);

        GameObject mesh = currentEntity.transform.Find("Mesh Holder").gameObject;
        mesh.transform.localScale = Vector3.one * radius;
        mesh.GetComponent<TerrainGenerator>().material = new Material(shader);
        mesh.GetComponent<TerrainGenerator>().material.color = color;

        /*if (radius > 5000)
        {
            radius /= 2;
        }
        if (orbitRadius > 100000)
        {
            orbitRadius /= 1.5f;
        }*/


        GameObject go = GameObject.Find("Sun");
        CelestialBody massBody = go.GetComponent<CelestialBody>();
        Vector3 initialVelocity = new Vector3(0, 1, 0) * (float)(Mathf.Sqrt((Universe.gravitationalConstant * massBody.mass / (orbitRadius + body.orbitRadius)) * 1.5f));

        CelestialBody createdBody = currentEntity.GetComponent<CelestialBody>();
        createdBody.CreateBody(radius, orbitRadius, surfaceGravity, initialVelocity, body, bodyName);
        TerrainGenerator createMesh = mesh.GetComponent<TerrainGenerator>();
        createMesh.CreateMesh(true);
        NBodySimulation newBody = FindObjectOfType<NBodySimulation>();
        newBody.NewBody();
    }
}