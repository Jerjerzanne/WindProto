using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private bool flightMode = false;
    private float fMomentum = 0.0F;
    private float dMomentum = 0.0F;
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            flightMode = false;
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                fMomentum = Input.GetAxis("Vertical") * speed;

                moveDirection.y = jumpSpeed;
            }
        }

        if (!controller.isGrounded)
        {
            if (Input.GetKeyDown("e"))
            {
                flightMode = !flightMode;

            }

            if (flightMode)
            {
                float cameraAngle = GameObject.FindGameObjectWithTag("MainCamera").transform.eulerAngles.x;
                float angleBoostD = 0.0F;
                float angleBoostF = 0.0F;
                if (cameraAngle < 90 && cameraAngle > 0)
                {
                    cameraAngle = cameraAngle / 90;
                    angleBoostD = cameraAngle;
                    angleBoostF = 1 - cameraAngle;
                }
                else
                {
                    cameraAngle = cameraAngle % 90 /90;
                    angleBoostD = 1 - cameraAngle;
                    angleBoostF = cameraAngle;
                }
                Debug.Log("dBoost: " + angleBoostD);
                Debug.Log("fBoost: " + angleBoostF);
                dMomentum = moveDirection.y - gravity * Time.deltaTime * angleBoostD;
                fMomentum = fMomentum + gravity * Time.deltaTime * angleBoostF;
                moveDirection = new Vector3(Input.GetAxis("Horizontal")* speed, dMomentum, fMomentum );
                moveDirection = transform.TransformDirection(moveDirection);
                //moveDirection *= speed;
                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                }
                
            }
            
        }

        if (flightMode)
        {
            //moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }
    }

    void OnGUI()
    {
        GUI.Button(new Rect(10, 10, 250, 40), "flightMode: "+flightMode);
        GUI.Button(new Rect(10, 50, 250, 40), "moveDirection: " + moveDirection);
        GUI.Button(new Rect(10, 90, 250, 40), "rotation: " + GameObject.FindGameObjectWithTag("MainCamera").transform.eulerAngles);
    }
}

