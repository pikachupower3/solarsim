using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent (typeof (Rigidbody))]
public class CelestialBody : GravityObject {

    public float radius;
    public float surfaceGravity;
   /* public float orbitRadius;
    public Transform targetObject;*/
    public Vector3 initialVelocity;
    public string bodyName = "Unnamed";
    Transform meshHolder;

    public Vector3 velocity { get; private set; }
    public float mass { get; private set; }
    public double mass2 { get; private set; }
    Rigidbody rb;

    void Awake () {
        rb = GetComponent<Rigidbody> ();
        rb.mass = mass;
        velocity = initialVelocity;
        /*transform.Translate(orbitRadius, 0, 0); */
    }

    public void UpdateVelocity (CelestialBody[] allBodies, float timeStep) {
        foreach (var otherBody in allBodies) {
            if (otherBody != this) {
                float sqrDst = (otherBody.rb.position - rb.position).sqrMagnitude;
                Vector3 forceDir = (otherBody.rb.position - rb.position).normalized;

                Vector3 acceleration = forceDir * Universe.gravitationalConstant * otherBody.mass / sqrDst;
                velocity += acceleration * timeStep;
            }
        }
    }

    public void UpdateVelocity (Vector3 acceleration, float timeStep) {
        velocity += acceleration * timeStep;
    }

    public void UpdatePosition (float timeStep) {
        rb.MovePosition (rb.position + velocity * timeStep);

    }

    void OnValidate () {
        mass2 = surfaceGravity * radius * radius / Universe.gravitationalConstant / 1e+16;
        mass = (float)mass2;
        meshHolder = transform.GetChild (0);
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
    void Update()
    {
        if (Input.GetKeyDown("h"))
        {
            Debug.Log("Mass = " + GetComponent<Rigidbody>().mass + gameObject.name);

        }
    }

}