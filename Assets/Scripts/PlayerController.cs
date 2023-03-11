using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // move
    public float speed = 15f;
    float horizontal;
    float vertical;
    public Transform orientation;
    Vector3 moveDirection;
    public float groundDrag;
    public float playerHeight;
    public LayerMask whatisGround;

    // jump
    bool isGrounded;
    /*public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;
    public KeyCode jumpKey = KeyCode.Space;*/
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public bool grounded;

    // rigidbody
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // rigidbody
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // move
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatisGround);
        MyInput();
        SpeedControl();
        if (isGrounded)
            rb.drag = groundDrag;
        else
        {
            rb.drag = 0;
        }

        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    //update
    private void MyInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // jump
        /*if (Input.GetKey(jumpKey) && readyToJump && isGrounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }*/
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * vertical + orientation.right * horizontal;
        if (isGrounded)
            rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
        else if (!isGrounded)
            rb.AddForce(moveDirection.normalized * speed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    /*private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }*/

    void OnCollisionStay()
    {
        isGrounded = true;
    }
}
