using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Control Script/Mouse Look")]
public class MouseLook : MonoBehaviour
{
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;
    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private float verticalRot = 0;

    // Reference to the camera
    private Transform cameraTransform;

    void Start()
    {
        // Find the camera in the hierarchy
        cameraTransform = transform.Find("Main Camera");
        if (cameraTransform == null)
        {
            Debug.LogError("Camera not found as a child of the player object.");
        }

    }

    void Update()
    {
        // Change in pitch
        verticalRot -= Input.GetAxis("Mouse Y") * sensitivityVert;
        verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);

        // Change in yaw
        float delta = Input.GetAxis("Mouse X") * sensitivityHor;
        float horizontalRot = transform.localEulerAngles.y + delta;

        // Set pitch and yaw for the camera and player separately
        cameraTransform.localEulerAngles = new Vector3(verticalRot, 0, 0);
        transform.localEulerAngles = new Vector3(0, horizontalRot, 0);
    }
}





