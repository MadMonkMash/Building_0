using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrow : MonoBehaviour { 

    public float grenadeImpulse = 10.0f;
    static float MAX_IMPULSE = 30.0f;
    public GameObject grenadePrefab;
    public GameObject cam;

    void Start ()
    {
        cam = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && grenadeImpulse < MAX_IMPULSE)  grenadeImpulse += 0.01f;
 
        //fire when released
        if (Input.GetMouseButtonUp(0)) fireGrenade();
    }

    void fireGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform);
        grenade.transform.position = cam.transform.position + cam.transform.forward * 2;
        Rigidbody target = grenade.GetComponent<Rigidbody>();
        Vector3 impulse = cam.transform.forward * grenadeImpulse;
        target.AddForceAtPosition(impulse, cam.transform.position, ForceMode.Impulse);
        grenadeImpulse = 10.0f;//reset impulse for next grenade
    }
}
