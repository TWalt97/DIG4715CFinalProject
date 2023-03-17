using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : MonoBehaviour
{

    // player
    private PlayerController playerController;

    // shrink size
    public float shrinkSize = 1f;

    // normal size
    public float normalSize = 2f;

    // speed for when we shrink
    //public float shrinkSpeed = 5f;

    // awake
    void Awake()
    {
        // player
        playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // shrink
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Debug.Log("Pressing left control");

            // shrink size
            playerController.transform.localScale = new Vector3 (shrinkSize, shrinkSize, shrinkSize);
        }

        // normal
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Debug.Log("Release left control");

            // return to normal size
            playerController.transform.localScale = new Vector3 (normalSize, normalSize, normalSize);
        }
    }
}
