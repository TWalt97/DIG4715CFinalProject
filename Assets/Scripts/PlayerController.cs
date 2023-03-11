using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // move
    public float speed = 15f;
    float horizontal;
    float vertical;
    // update
    public Transform orientation;
    Vector3 moveDirection;
    public float drag;
    public float playerHeight;
    public LayerMask whatisGround;


    // jump
    public bool isGrounded;

    // rigidbody
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // rigidbody
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        // move
        /*horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        vertical = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.Translate(horizontal, 0, vertical);*/
        //update
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatisGround);
        MyInput();
        

        // jump
        /*if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            isGrounded = false;
        }*/  
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
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * vertical + orientation.right * horizontal;
        rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
    }

    // jump 
    /*private void OnCollisionEnter(Collision collision)
    {
        // check ground
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }*/
}
