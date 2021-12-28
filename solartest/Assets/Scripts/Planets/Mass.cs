using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mass : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("Mass = " + GetComponent<Rigidbody>().mass);
    }

    void Update()
    {
        if (Input.GetKeyDown("h"))
        {
            Debug.Log("Mass = " + GetComponent<Rigidbody>().mass);

        }
    }
}
