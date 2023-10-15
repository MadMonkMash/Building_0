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
        ContactPoint contact = collision.contacts[0];
        Vector3 position = contact.point;
        AudioSource.PlayClipAtPoint(clip, position, 1);
        Destroy(gameObject);
        m.startDistraction(this.position);
    }
}
