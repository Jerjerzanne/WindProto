using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private bool flightMode = false;
    private float momentum = 0.0F;
    private GameObject camera;
    public bool showGUI = false;

    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        if (Input.GetKeyUp("o"))
            showGUI = !showGUI;

        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            flightMode = false;
            momentum = 0;
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        if (!controller.isGrounded)
        {
            if (Input.GetKeyDown("left shift"))
            {
                flightMode = true;
                momentum = controller.velocity.magnitude;
            }

            if (Input.GetKeyUp("left shift"))
            {
                flightMode = false;
                moveDirection = camera.transform.forward * momentum;
            }

            if (flightMode)
            {
                float cameraAngle = camera.transform.eulerAngles.x;
                float angleBoostD = 0.0F;
                if (cameraAngle < 90 && cameraAngle > 0)
                {
                    cameraAngle = cameraAngle / 90;
                    angleBoostD = cameraAngle;
                    momentum = momentum + angleBoostD * gravity * Time.deltaTime;
                }
                else
                {
                    cameraAngle = cameraAngle % 90 / 90;
                    angleBoostD = 1 - cameraAngle;
                    momentum = momentum - angleBoostD * gravity * Time.deltaTime;
                    if (momentum < 0)
                        momentum = 0;
                }

                if (Input.GetButtonDown("Jump"))
                {
                    momentum += jumpSpeed;
                }

            }

        }

        if (flightMode)
        {
            if (Input.GetKey("s"))
            {
                controller.Move(camera.transform.forward * momentum * 0.5F * Time.deltaTime);
            }
            else
            {
                controller.Move(camera.transform.forward * momentum * Time.deltaTime);
            }
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }
    }

    void OnGUI()
    {
        if (showGUI == true)
        {
            GUI.Button(new Rect(10, 10, 250, 40), "flightMode: " + flightMode);
            GUI.Button(new Rect(10, 50, 250, 40), "moveDirection: " + moveDirection);
            GUI.Button(new Rect(10, 90, 250, 40), "rotation: " + camera.transform.rotation);
            GUI.Button(new Rect(10, 130, 250, 40),
                "velocity: " + GetComponent<CharacterController>().velocity.magnitude);
        }
    }


}
