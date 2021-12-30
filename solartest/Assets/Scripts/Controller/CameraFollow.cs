using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothness;
    public Transform targetObject;
    private Vector3 initalOffset;
    private Vector3 cameraPosition;

    public enum RelativePosition { InitalPosition, Position1, Position2 }
    public RelativePosition relativePosition;
    public Vector3 position1;
    public Vector3 position2;
    public Vector3 initialPosition;

    private float moveSpeed = 500f;
    private float scrollSpeed = 500f;

    void Start()
    {
        relativePosition = RelativePosition.InitalPosition;
        initalOffset = transform.position - targetObject.position;
        initialPosition = transform.position;
    }

    void FixedUpdate()
    {
        cameraPosition = targetObject.position + CameraOffset(relativePosition);
        transform.position = Vector3.Lerp(transform.position, cameraPosition, smoothness * Time.fixedDeltaTime);

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.position += moveSpeed * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }

        if (Input.GetAxis("Up/Down") != 0)
        {
            transform.position += scrollSpeed * new Vector3(0, -Input.GetAxis("Up/Down"), 0);
        }

        if (Input.GetAxis("LookAtTarget") != 0)
        {
            transform.LookAt(targetObject);
        }

        if (Input.GetKeyDown("f"))
        {
            transform.position = initialPosition;
        }
    }

        Vector3 CameraOffset(RelativePosition ralativePos)
    {
        Vector3 currentOffset;

        switch (ralativePos)
        {
            case RelativePosition.Position1:
                currentOffset = position1;
                break;

            case RelativePosition.Position2:
                currentOffset = position2;
                break;

            default:
                currentOffset = initalOffset;
                break;
        }
        return currentOffset;
    }
}
