using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject display;

    void OnTriggerEnter()
    {
        display.SetActive(true);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
