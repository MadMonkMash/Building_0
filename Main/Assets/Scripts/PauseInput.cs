using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PauseInput: MonoBehaviour
{
    private CharacterController charController;
    private PauseMenuController pauseMenu;

    void Start()
    {
        charController = GetComponent<CharacterController>();
        pauseMenu = FindObjectOfType<PauseMenuController>();

        // Hide pause menu on start
        pauseMenu.HidePauseMenu();
    }

    void Update()
    {

        // Check if tab is pressed
        if (Input.GetKeyDown(KeyCode.Tab))
        {

            // Check if game is paused
            bool isPaused = pauseMenu.getIsPaused();

            if (!isPaused)
            {

                // Pause game if game is not paused
                isPaused = true;
                pauseMenu.DisplayPauseMenu();

            }
            else
            {

                // Resume game is unpaused
                isPaused = false;
                pauseMenu.HidePauseMenu();
            }
        }
    }
}
