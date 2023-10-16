using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorEvent : MonoBehaviour
{
    public GameObject display;
    public GameObject player;
    public bool isOpen = false;


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Door trigger");
        if (other.CompareTag("Player") && display.activeSelf)
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        if (!isOpen)
        {
            SceneManager.LoadScene(3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
