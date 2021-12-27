using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessManager : MonoBehaviour {

    public float distanceThreshold = 1000;
    List<Transform> physicsObjects;

    void Awake () {
        var bodies = FindObjectsOfType<CelestialBody> ();

        physicsObjects = new List<Transform> ();
        foreach (var c in bodies) {
            physicsObjects.Add (c.transform);
        }
    }
}