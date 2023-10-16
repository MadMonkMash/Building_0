using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrow : MonoBehaviour { 

    public float grenadeImpulse = 10.0f;
    static float MAX_IMPULSE = 30.0f;
    public GameObject grenadePrefab;
    public GameObject cam;
    public bool hasRock = false;

    void Start ()
    {
        cam = GameObject.Find("Main Camera");
    }

    void Update()
    {
        //charge while held
        if (Input.GetMouseButton(0) && grenadeImpulse < MAX_IMPULSE)  grenadeImpulse += 0.01f;
 
        //fire when released
        if (Input.GetMouseButtonUp(0)) fireGrenade();
    }

    public void setRock()
    {
        hasRock = true;
    }

    void fireGrenade()
    {
        if (hasRock)
        {
            //create prefab in front of camera
            GameObject grenade = Instantiate(grenadePrefab, transform);
            grenade.transform.position = cam.transform.position + cam.transform.forward * 2;
            //launch 
            Rigidbody target = grenade.GetComponent<Rigidbody>();
            Vector3 impulse = cam.transform.forward * grenadeImpulse;
            target.AddForceAtPosition(impulse, cam.transform.position, ForceMode.Impulse);
            //reset charge impulse for next grenade
            grenadeImpulse = 10.0f;
            hasRock = false;
        }
    }
}
