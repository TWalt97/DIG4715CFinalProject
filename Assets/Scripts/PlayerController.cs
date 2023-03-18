using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 15f;
    public float rotationSmoothTime;

    public float gravity = 9.8f;
    public float gravityMultiplier = 2;
    public float groundedGravity = -0.5f;
    public float jumpHeight = 3f;
    float velocityY;

    public CharacterController controller;
    public Camera cam;

    float currentAngle;
    float currentAngleVelocity;

    private void Awake()
    {
        //getting reference for components on the Player
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    private void Update()
    {
        HandleMovement();
        HandleGravityAndJump();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            //compute rotation
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref currentAngleVelocity, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0, currentAngle, 0);

            //move in direction of rotation
            Vector3 rotatedMovement = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(rotatedMovement * speed * Time.deltaTime);
        }
    }

    void HandleGravityAndJump()
    {
        //apply groundedGravity when the Player is Grounded
        if (controller.isGrounded && velocityY < 0f)
            velocityY = groundedGravity;

        //When Grounded and Jump Button is Pressed, set veloctiyY with the formula below
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocityY = Mathf.Sqrt(jumpHeight * 2f * gravity);
        }

        //applying gravity when Player is not grounded
        velocityY -= gravity * gravityMultiplier * Time.deltaTime;
        controller.Move(Vector3.up * velocityY * Time.deltaTime);
    }
}
