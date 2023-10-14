using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PauseMenuController : MonoBehaviour
{
    public CanvasGroup pauseCanvasGroup = null;

    public Button resumeButton = null;

    public bool isPaused = false;

    public bool getIsPaused()
    {
        return isPaused;
    }

    public void HidePauseMenu()
    {
        pauseCanvasGroup.alpha = 0;
        pauseCanvasGroup.blocksRaycasts = false;
        pauseCanvasGroup.interactable = false;

        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isPaused = false;
    }

    public void DisplayPauseMenu()
    {
        pauseCanvasGroup.alpha = 1;
        pauseCanvasGroup.blocksRaycasts = true;
        pauseCanvasGroup.interactable = true;

        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        isPaused = true;
    }

    public void OnResumeButton()
    {
        HidePauseMenu();
    }

    public void OnExitButton()
    {
        SceneManager.LoadScene(0);
    }
}
