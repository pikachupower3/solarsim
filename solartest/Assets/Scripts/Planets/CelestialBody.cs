using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Rigidbody))]
public class CelestialBody : CreationManager
{

   /* public float radius;
    public float orbitRadius;
    public float surfaceGravity;*/
    public float mass;
    public CelestialBody targetObject;
    public Vector3 initialVelocity;
    public string bodyName = "Unnamed";
    Transform meshHolder;

    public Vector3 velocity { get; private set; }
    public float massPlanet { get; private set; }
    public float massStar { get; private set; }
    Rigidbody rb;

    void Awake() {
        rb = GetComponent<Rigidbody>();
        rb.mass = mass;
        velocity = initialVelocity;
        transform.position = new Vector3 (-orbitRadius - targetObject.orbitRadius, 0, 0);
        /*massPlanet = 0;
        massStar = 0;*/
    }

    public void UpdateVelocity(CelestialBody[] allBodies, float timeStep) {
        foreach (var otherBody in allBodies) {
            if (otherBody != this) {
                float sqrDst = (otherBody.rb.position - rb.position).sqrMagnitude;
                Vector3 forceDir = (otherBody.rb.position - rb.position).normalized;

                Vector3 acceleration = forceDir * Universe.gravitationalConstant * otherBody.mass / sqrDst;
                velocity += acceleration * timeStep;
            }
        }
    }

    public void UpdateVelocity(Vector3 acceleration, float timeStep) {
        velocity += acceleration * timeStep;
    }

    public void UpdatePosition(float timeStep) {
        rb.MovePosition(rb.position + velocity * timeStep);

    }
    
    public void CreateBody(string name)
    {
        bodyName = name;
    }

    void OnValidate()
    {
        if (gameObject.name != "Sun")
        {
            massPlanet = (float)(surfaceGravity * radius * radius / Universe.gravitationalConstant);
        }
        else
        {
            massStar = (float)(surfaceGravity * radius * radius * 2.5 / Universe.gravitationalConstant);
        }
        mass = massPlanet + massStar;
        meshHolder = transform.GetChild(0);
        meshHolder.localScale = Vector3.one * radius;
        gameObject.name = bodyName;
    }

    public Rigidbody Rigidbody {
        get {
            return rb;
        }
    }

    public Vector3 Position {
        get {
            return rb.position;
        }
    }
}