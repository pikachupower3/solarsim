using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationHelper : CreationManager
{
    GameObject planet;
    GameObject meshHandler;
    GameObject lineHandler;

    void Update()
    {
        if (Input.GetKeyDown("h"))
        {
            GameObject planet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            planet.transform.position = new Vector3(0, 1.5f, 0);
            meshHandler = new GameObject("Mesh Holder");
            lineHandler = new GameObject("Line renderer");

            planet.transform.SetParent(this.transform.parent);
            meshHandler.transform.SetParent(planet.transform);
            lineHandler.transform.SetParent(planet.transform);
            meshHandler.AddComponent<TerrainGenerator>().terrainResolution = 50;
            meshHandler.GetComponent<TerrainGenerator>().material = Material;
            lineHandler.AddComponent<LineRenderer>().material = Line;
            planet.AddComponent<Rigidbody>();
            planet.AddComponent<CelestialBody>().bodyName = name;
            planet.GetComponent<CelestialBody>().radius = radius;
            planet.GetComponent<CelestialBody>().orbitRadius = orbitRadius;
            planet.GetComponent<CelestialBody>().surfaceGravity = surfaceGravity;
            planet.transform.position = new Vector3(-planet.GetComponent<CelestialBody>().orbitRadius, 0, 0);
        }
    }
}
