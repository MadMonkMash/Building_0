using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIVis : MonoBehaviour
{
    public GameObject key;
    public GameObject bar;
    // Start is called before the first frame update
    void Start()
    {
        key.SetActive(false);
        bar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
