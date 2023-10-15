using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    public AudioClip clip;

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Vector3 position = contact.point;
        AudioSource.PlayClipAtPoint(clip, position, 1);
        StartCoroutine(MonsterScript.Distraction(position));
        Destroy(gameObject);
    }
}
