using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CemraController : MonoBehaviour
{
    // move
    public float speed = 15f;
    float horizontal;
    float vertical;
    // changes
    public Transform orientation;
    public Transform player;
    public Transform playerObj;

    // Start is called before the first frame update
    void Start()
    {
        // update
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // update
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;
        // move
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        //transform.Translate(horizontal, 0, vertical);
        //update
        Vector3 inputDir = orientation.forward * vertical + orientation.right * horizontal;
        if (inputDir != Vector3.zero)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * speed);
        } 
    }
}
