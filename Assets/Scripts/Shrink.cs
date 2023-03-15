using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : MonoBehaviour
{
    // move
    public Transform target;

    // player gameobjects
    public GameObject normal;
    public GameObject shrink;

    // player
    private PlayerController playerController;

    // awake
    void Awake()
    {
        // player
        playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        var targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, playerController.speed);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Debug.Log("Pressing left control");
            // activate playerB
            shrink.SetActive(true);

            // deactivate playerA
            normal.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Debug.Log("Release left control");
            // activate playerA
            normal.SetActive(true);

            // deactivate playerB
            shrink.SetActive(false);
        }
    }
}
