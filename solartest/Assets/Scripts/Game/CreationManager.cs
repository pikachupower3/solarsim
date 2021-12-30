using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationManager : MonoBehaviour
{
    public Material Line { get; private set; }
    public float radius;
    public float orbitRadius;
    public float surfaceGravity;
    public Material Material;
    public string name;

    void Awake()
    {
        Line = Resources.Load("UI/Line", typeof(Material)) as Material;
    }

    

}
