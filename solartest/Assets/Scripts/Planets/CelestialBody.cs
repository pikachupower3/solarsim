using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Rigidbody))]
public class CelestialBody : GravityObject
{
    public float radius;
    public float orbitRadius;
    public float surfaceGravity;
    public float mass;
    public Vector3 initialVelocity;
    public CelestialBody orbitBody;
    public string bodyName = "Unnamed";
    Transform meshHolder;

    public Vector3 velocity { get; private set; }
    public float massPlanet { get; private set; }
    public float massStar { get; private set; }
    Rigidbody rb;

    public void Awake() {
        rb = GetComponent<Rigidbody>();
        rb.mass = mass;
        velocity = initialVelocity;

        transform.localPosition = new Vector3 (-orbitRadius, 0, 0);
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

    public void CreateBody(float radiusCreate, float orbitRadiusCreate, float surfaceGravityCreate, Vector3 initvelocity, CelestialBody orbittingBody, string name)
    {
        radius = radiusCreate;
        orbitRadius = orbitRadiusCreate;
        surfaceGravity = surfaceGravityCreate;
        initialVelocity = initvelocity;
        orbitBody = orbittingBody;
        bodyName = name;
        OnValidate();
        Awake();
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
