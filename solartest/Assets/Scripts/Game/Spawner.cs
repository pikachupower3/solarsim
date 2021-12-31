using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Planet; 
    public float radius;
    public float orbitRadius;
    public float surfaceGravity;
    public Material Texture;
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
        mesh.GetComponent<TerrainGenerator>().material = Texture;

        if (radius > 5000)
        {
            radius /= 2;
        }
        if (orbitRadius > 100000)
        {
            orbitRadius /= 2;
        }

        CelestialBody createdBody = currentEntity.GetComponent<CelestialBody>();
        createdBody.CreateBody(radius, orbitRadius, surfaceGravity, bodyName);
        TerrainGenerator createMesh = mesh.GetComponent<TerrainGenerator>();
        createMesh.CreateMesh(true);
    }
}