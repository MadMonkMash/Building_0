using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float runSpeed = 12.0f;
    public bool canMove = true;

    // Reference to the character controller
    private CharacterController charController;

    void Start()
    {
        // Get the character controller component
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float moveSpeed = canMove ? (isRunning ? runSpeed : speed) : 0;

        float deltaZ = moveSpeed * Input.GetAxis("Vertical");
        float deltaX = moveSpeed * Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);

        // Add gravity to simulate realistic vertical movement
        movement.y = Physics.gravity.y * Time.deltaTime;

        // Ensure movement is independent of the framerate
        movement *= Time.deltaTime;

        // Transform from local space to global space
        movement = transform.TransformDirection(movement);

        // Pass the movement to the character controller
        charController.Move(movement);
    }
}






