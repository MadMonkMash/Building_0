using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    public AudioClip clip;
    public MonsterScript m;
    Vector3 position;

    void Start()
    {
        m = GameObject.Find("Monster").GetComponent<MonsterScript>();
    }

    void OnCollisionEnter(Collision collision)
    {
        //find collision 
        ContactPoint contact = collision.contacts[0];
        Vector3 position = contact.point;
        //play sound
        AudioSource.PlayClipAtPoint(clip, position, 1);
        //remove rock and cause monster to path
        m.startDistraction(position);
        Destroy(gameObject);
        
    }
}
