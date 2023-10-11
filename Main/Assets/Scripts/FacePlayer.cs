using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v3 = player.position - transform.position;
        v3.y = 90f;
        transform.rotation = Quaternion.LookRotation(-v3);

    }
}
