using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private bool flightMode = false;
    private float momentum = 0.0F;
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
                momentum = Input.GetAxis("Vertical");
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
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, momentum);
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;
                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                }
                
            }
            
        }
        //    moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        //    moveDirection = transform.TransformDirection(moveDirection);
        //    moveDirection *= speed;
        //    if (Input.GetButton("Jump"))
        //    {
        //        moveDirection.y = jumpSpeed;
        //    }
        //}

        if (flightMode)
        {
            moveDirection.y -= 0.5f * gravity * Time.deltaTime;
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
       
    }
}

