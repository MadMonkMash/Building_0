using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    public float crouchSpeed = 3.0f;
    public float walkSpeed = 6.0f;
    public float sprintSpeed = 12.0f;
    public bool canMove = true;

    private CharacterController charController; // Reference to the character controller
    private GameObject cam;

    public float crouchHeight = 0.5f;
    public float crouchCameraOffset = -0.5f;
    public float crouchTransitionSpeed = 10f;

    private float originalHeight;
    private Vector3 cameraStandPosition;
    private Vector3 cameraCrouchPosition;

    public float maxStamina = 100.0f;
    public float staminaDrainRate = 100.0f;
    public float staminaRechargeRate = 20.0f;

    private float currentStamina;
    private StaminaUI staminaUI;

    void Start()
    {
        // Get the character controller component
        charController = GetComponent<CharacterController>();

        // Set starting stamina to max
        currentStamina = maxStamina;

        // Find instance of StaminaUI
        staminaUI = FindObjectOfType<StaminaUI>();

        // Set stamina UI to hide on start
        staminaUI.UpdateStamina(currentStamina, maxStamina, 0);

        cam = GameObject.Find("Main Camera");

        //get starting player height for uncrouching
        originalHeight = charController.height;

        //define camera positions
        cameraStandPosition = cam.transform.localPosition;
        cameraCrouchPosition = cameraStandPosition + new Vector3(0, crouchCameraOffset, 0);
    }

    void Update()
    {
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        bool isCrouching = Input.GetKey(KeyCode.C);
        float moveSpeed;

        if (isSprinting && !isCrouching)
        {
            moveSpeed = sprintSpeed;
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, cameraStandPosition, crouchTransitionSpeed * Time.deltaTime);
            currentStamina -= staminaDrainRate * Time.deltaTime;
            staminaUI.UpdateStamina(currentStamina, maxStamina, 1);

            if (currentStamina < 0)
            {
                moveSpeed = walkSpeed;
                currentStamina = 0;
                Debug.Log("Stamina empty");
            }
        }
        else if (isCrouching)
        {
            charController.height = crouchHeight;
            moveSpeed = crouchSpeed;
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, cameraCrouchPosition, crouchTransitionSpeed * Time.deltaTime);
        }
        else
        {
            moveSpeed = walkSpeed;
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, cameraStandPosition, crouchTransitionSpeed * Time.deltaTime);
            currentStamina += staminaRechargeRate * Time.deltaTime;
            staminaUI.UpdateStamina(currentStamina, maxStamina, 1);
        }

        float deltaZ = moveSpeed * Input.GetAxis("Vertical");
        float deltaX = moveSpeed * Input.GetAxis("Horizontal");

        Vector3 movement = transform.forward * deltaZ + transform.right * deltaX;
        movement.y = 0;

        // Apply gravity
        /*
        if (!charController.isGrounded)
        {
            movement.y = Physics.gravity.y;
        }
        */

        movement *= Time.deltaTime;

        charController.Move(movement);

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        if (currentStamina == maxStamina)
        {
            staminaUI.UpdateStamina(currentStamina, maxStamina, 0);
        }
    }
}