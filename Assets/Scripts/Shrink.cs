using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : MonoBehaviour
{
    [Header("Player")]
    private PlayerController playerController;

    [Header("Size")]
    public float shrinkSize;
    public float normalSize;
    public bool shrinkBool = false;

    // [Header("Animator")]
    // Animator animator;

    void Awake()
    {
        // player
        playerController = GameObject.FindObjectOfType<PlayerController>();
        // animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shrinkBool == true)
        {
            // AudioManager.Instance.PlaySFX("Shrink");
            // Debug.Log("Pressing left control");

            // shrink size
            playerController.transform.localScale = new Vector3 (shrinkSize, shrinkSize, shrinkSize);
            // animator.SetBool("Shrink", true);
            // Invoke("ResetAnim", 0.5f);
        }
        if (shrinkBool == false)
        {
            // Debug.Log("Release left control");

            // return to normal size
            playerController.transform.localScale = new Vector3 (normalSize, normalSize, normalSize);
            // animator.SetBool("Shrink", true);
            // Invoke("ResetAnim", 0.5f);
        }
        // shrink
        /*if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            AudioManager.Instance.PlaySFX("Shrink");
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
        }*/
    }

    void OnShrink()
    {
        shrinkBool = !shrinkBool;
    }

//     void ResetAnim()
//     {
//         animator.SetBool("Shrink", false);
//     }
}
