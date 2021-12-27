using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    private float moveSpeed = 50f;
    private float scrollSpeed = 50f;

    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.position += moveSpeed * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }

        if (Input.GetAxis("Up/Down") != 0)
        {
            transform.position += scrollSpeed * new Vector3(0, -Input.GetAxis("Up/Down"), 0);
        }
    }

}