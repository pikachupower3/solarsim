using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NBodySimulation : MonoBehaviour {
    CelestialBody[] bodies;
    static NBodySimulation instance;

    /*Used to add the new planet created through the Spawner script to the array*/
    public void NewBody()
    {
        bodies = FindObjectsOfType<CelestialBody>();
    }

    /*On start set values*/
    void Awake () {

        bodies = FindObjectsOfType<CelestialBody> ();
        Time.fixedDeltaTime = Universe.physicsTimeStep;
    }

    /*Update every frame*/
    void FixedUpdate () {
        for (int i = 0; i < bodies.Length; i++) {
            Vector3 acceleration = CalculateAcceleration (bodies[i].Position, bodies[i]);
            bodies[i].UpdateVelocity (acceleration, Universe.physicsTimeStep);
            //bodies[i].UpdateVelocity (bodies, Universe.physicsTimeStep);
        }

        for (int i = 0; i < bodies.Length; i++) {
            bodies[i].UpdatePosition (Universe.physicsTimeStep);
            
            if (Input.GetKeyDown("j"))
            {
                Debug.Log("Bodies = " + bodies[i] + " " + i);
            }
        }


    }

    /*Calls function from Celestialbody, calculates the velocity for the body based on the mass and distance to all other bodies*/
    public static Vector3 CalculateAcceleration (Vector3 point, CelestialBody ignoreBody = null) {
        Vector3 acceleration = Vector3.zero;
        foreach (var body in Instance.bodies) {
            if (body != ignoreBody) {
                float sqrDst = (body.Position - point).sqrMagnitude;
                Vector3 forceDir = (body.Position - point).normalized;
                acceleration += forceDir * Universe.gravitationalConstant * body.mass / sqrDst;
            }
        }

        return acceleration;
    }

    public static CelestialBody[] Bodies {
        get {
            return Instance.bodies;
        }
    }

    static NBodySimulation Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<NBodySimulation> ();
            }
            return instance;
        }
    }
}
