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
        pauseMenu.HidePauseMenu();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {

            bool isPaused = pauseMenu.getIsPaused();

            if (!isPaused)
            {
                isPaused = true;
                pauseMenu.DisplayPauseMenu();

            }
            else
            {
                isPaused = false;
                pauseMenu.HidePauseMenu();
            }
        }
    }
}
