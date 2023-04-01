using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : MonoBehaviour
{
    [Header("Player")]
    private PlayerController playerController;

    [Header("Size")]
    public float shrinkSize = 1f;
    public float normalSize = 2f;

    Animator animator;

    // awake
    void Awake()
    {
        // player
        playerController = GameObject.FindObjectOfType<PlayerController>();
        animator = GetComponentInChildren<Animator>();
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
            animator.SetBool("Shrink", true);
            Invoke("ResetAnim", 0.5f);
        }

        // normal
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Debug.Log("Release left control");

            // return to normal size
            playerController.transform.localScale = new Vector3 (normalSize, normalSize, normalSize);
            animator.SetBool("Shrink", true);
            Invoke("ResetAnim", 0.5f);
        }
    }

    void ResetAnim()
    {
        animator.SetBool("Shrink", false);
    }
}
