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

    // jump
    //public bool isGrounded = true;

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
