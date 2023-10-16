using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateEvent : MonoBehaviour
{

    public int speed = 7; //if this is a float it breaks lol
    public Vector3 target = new Vector3(0f, 14.3f, 0f);
    private bool isOpen = false;
    public GameObject gate;
    public AudioSource gateAudioSource;
    // Start is called before the first frame update
    void Start()
    {
            
    }
    // calls slide gate when player collides and gate is closed
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered Elevator");

        if (other.CompareTag("Player") && !isOpen)
        {
            // move the gate towards the target position and set isOpen
            StartCoroutine(SlideGate(target));
            isOpen = true;

            if (gateAudioSource != null)
            {
                gateAudioSource.Play();
            }
        }
    }
    // Slides gate to position when called

    private IEnumerator SlideGate(Vector3 target)
    {
        float startTime = Time.time;
        Vector3 initialPosition = gate.transform.localPosition;

        //lerp for speed duration
        while (Time.time - startTime < speed)
        {
            float t = (Time.time - startTime);
            gate.transform.localPosition = Vector3.Lerp(initialPosition, target, t / speed);
            yield return null;
        }

        gate.transform.localPosition = target;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
