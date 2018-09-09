using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private bool flightMode = false;

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
                moveDirection.y = jumpSpeed;
            }
        }

        //if (!controller.isGrounded)
        //{
        //    if (Input.GetKeyDown("E"))
        //    {
        //        flightMode = !flightMode;
        //    }
           
        //    moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        //    moveDirection = transform.TransformDirection(moveDirection);
        //    moveDirection *= speed;
        //    if (Input.GetButton("Jump"))
        //    {
        //        moveDirection.y = jumpSpeed;
        //    }
        //}

        //if (flightMode)
        //{
        //    moveDirection.y -= 0.5f * gravity * Time.deltaTime;
        //    controller.Move(moveDirection * Time.deltaTime);
        //}
        //else
        //{
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        
    }

    void OnGUI()
    {
        GUI.Button(new Rect(10, 10, 150, 100), "flightMode: "+flightMode);
    }
}

