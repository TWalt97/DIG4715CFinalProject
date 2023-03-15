using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // move
    public float speed = 15f;
    /*public bool walking;
    public Transform playerTrans;*/

    // rigidbody
    Rigidbody rb;

    // jump
    public bool isGrounded;
    public float jumpForce = 100f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    /*void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = -transform.forward * speed * Time.deltaTime;
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        // move
        /*if (Input.GetKeyDown(KeyCode.W))
        {
            walking = true;
            //steps1.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            walking = false;
            //steps1.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //steps1.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            //steps1.SetActive(false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerTrans.Rotate(0, -speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerTrans.Rotate(0, speed * Time.deltaTime, 0);
        }*/

        float horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float vertical = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.Translate(horizontal, 0, vertical);

        // jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
}
