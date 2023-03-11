using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // move
    public float speed = 15f;
    float horizontal;
    float vertical;
    // changes
    public Transform orientation;
    public Transform player;
    public Transform playerObj;

    // jump
    public bool isGrounded = true;

    // rigidbody
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // rigidbody
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // update
        Vector3 viewDir = player.position - new Vector3(transform.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;
        // move
        horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        vertical = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.Translate(horizontal, 0, vertical);

        // jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            isGrounded = false;
        }  
    }

    // jump 
    private void OnCollisionEnter(Collision collision)
    {
        // check ground
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}
