using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPickup : MonoBehaviour
{
    public RockThrow rock;

    void Start()
    {
        rock = GameObject.Find("Player").GetComponent<RockThrow>();
    }

    void onTriggerEnter(Collider other)
    {
        rock.setRock();
        Debug.Log("rock picked up");
        Destroy(gameObject);
    }

}
