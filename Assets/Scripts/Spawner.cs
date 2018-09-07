using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public string spawnerKey;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnerKey != "")
        { 
            if (Input.GetKey("left shift") && Input.GetKeyDown(spawnerKey))
            {
                Debug.Log(GameObject.Find("Player").transform.position);
                GameObject.Find("Player").transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
        }
    }
}
