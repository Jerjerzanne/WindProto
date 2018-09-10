using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{

    public float horizontalSpeed = 2.0F;
    public float verticalSpeed = 2.0F;
    public float lowerAngle = 45.0F;
    public float upperAngle = 315.0F;
    private GameObject camera;


    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(camera.transform.position, camera.transform.forward, Color.green);

        float horizontalShift = horizontalSpeed * Input.GetAxis("Mouse X");
        float verticalShift = verticalSpeed * Input.GetAxis("Mouse Y");
        transform.Rotate(0, horizontalShift, 0);
        float xAngle = camera.transform.eulerAngles.x;
        float predictedRotation = 0;
        if (verticalShift > 0 && xAngle >= upperAngle) // moving up while looking up
        {
            predictedRotation = xAngle - verticalShift;
            Debug.Log(predictedRotation);
            if (predictedRotation <= upperAngle)
            {
                camera.transform.Rotate(upperAngle - xAngle, 0, 0);
            }
            else
            {  
                camera.transform.Rotate(-verticalShift, 0 , 0);
            }
        }
        else if (verticalShift > 0 && xAngle <= lowerAngle)  // moving up while looking down
        {
            predictedRotation = xAngle - verticalShift;
            if (predictedRotation < 0 && predictedRotation <= (upperAngle - 360.0f))
            {
                camera.transform.Rotate(xAngle - upperAngle, 0, 0);
            }
            else
            {
                camera.transform.Rotate(-verticalShift, 0, 0);
            }
        }
        else if (verticalShift < 0 && xAngle >= upperAngle) // moving down while looking up
        { 
            predictedRotation = xAngle - verticalShift;
            if (predictedRotation > 360 && (predictedRotation % 360 > lowerAngle))
            {
                camera.transform.Rotate(lowerAngle- xAngle, 0, 0);
            }
            else
            {
                camera.transform.Rotate(-verticalShift, 0, 0);
            }
        }
        else if (verticalShift < 0 && xAngle <= lowerAngle) // moving down while looking down
        {
            predictedRotation = xAngle - verticalShift;
            if (predictedRotation >= lowerAngle)
            {
                camera.transform.Rotate(lowerAngle - xAngle - 0.01f, 0, 0);
            }
            else
            {
                camera.transform.Rotate(-verticalShift, 0, 0);
            }
        }
       
    }

    //void OnGUI()
    //{
    //    float h = horizontalSpeed * Input.GetAxis("Mouse X");
    //    float v = verticalSpeed * Input.GetAxis("Mouse Y");
    //    GUI.Button(new Rect(10, 100, 200, 50), "zAngle: " + camera.transform.eulerAngles.z);
    //    GUI.Button(new Rect(10, 150, 200, 50), "xAngle: " + camera.transform.eulerAngles.x);
    //    GUI.Button(new Rect(10, 200, 100, 50), "h: " + h);
    //    GUI.Button(new Rect(10, 250, 100, 50), "v: " + v);
    //}
}

//Debug.DrawRay(camera.transform.position, camera.transform.forward, Color.green);

//float horizontalShift = horizontalSpeed * Input.GetAxis("Mouse X");
//float verticalShift = verticalSpeed * Input.GetAxis("Mouse Y");
//transform.Rotate(0, horizontalShift, 0);

//camera.transform.Rotate(-verticalShift, 0, 0);
//float xAngle = camera.transform.eulerAngles.x;
//Debug.Log(verticalShift+ xAngle);
//float fixAngle;
//if (xAngle > upperAngle && xAngle< 90)
//{
//fixAngle = xAngle - 45;
//camera.transform.Rotate(-fixAngle, 0, 0);
//}
//else if (xAngle<lowerAngle && xAngle> 180)
//{
//fixAngle = 315 - xAngle;
//camera.transform.Rotate(fixAngle, 0, 0);
//}