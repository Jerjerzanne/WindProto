using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
     public float horizontalSpeed = 2.0F;
    public float verticalSpeed = 2.0F;
    public float upperAngle = 45.0F;
    public float lowerAngle = 315.0F;
   
    void Update() {
        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        float v = verticalSpeed * Input.GetAxis("Mouse Y");

        transform.Rotate(0, h, 0);
        GameObject.FindGameObjectWithTag("MainCamera").transform.Rotate(-v,0,0);
        float zee = GameObject.FindGameObjectWithTag("MainCamera").transform.eulerAngles.z;
        float xee = GameObject.FindGameObjectWithTag("MainCamera").transform.eulerAngles.x;
        Debug.Log(xee);
        float boi;
        if (xee > upperAngle && xee < 180)
        {
            boi = xee - 45;
            GameObject.FindGameObjectWithTag("MainCamera").transform.Rotate(-boi, 0, -zee);
        }
        else if (xee < lowerAngle && xee > 180)
        {
            boi = 315-xee;
            GameObject.FindGameObjectWithTag("MainCamera").transform.Rotate(boi, 0, -zee);
        }
        else {
            GameObject.FindGameObjectWithTag("MainCamera").transform.Rotate(0, 0, -zee);
    }
    }
            
	
}
