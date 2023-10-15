using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
