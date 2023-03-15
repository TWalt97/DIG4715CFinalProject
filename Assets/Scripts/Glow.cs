using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour
{
    // move
    public Transform target;

    // player gameobjects
    public GameObject normal;
    public GameObject glow;

    // player
    private PlayerController playerController;

    // ability to be active for 5 seconds
    public float isGlowing = 10f;
    public bool glowBool = false;

    // cooldown
    public float cooldownTime = 10f;
    private float nextFireTime = 0;

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

        if (Time.time > nextFireTime)
        {
            if (Input.GetKey(KeyCode.E) && !glowBool)
            {
                // cooldown
                nextFireTime = Time.time + cooldownTime;

                // ability in use again
                StartCoroutine(Ability(isGlowing));
            }
            /*if (Input.GetKeyUp(KeyCode.E))
            {
                Debug.Log("Release E");
                // activate playerA
                normal.SetActive(true);

                // deactivate playerB
                glow.SetActive(false);
            }*/
        }
    }

    IEnumerator Ability(float isGlowing)
    {
        Debug.Log("Glow started");

        // set glow to true
        glowBool = true;

        // activate glow
        glow.SetActive(true);

        // deactivate normal
        normal.SetActive(false);

        yield return new WaitForSeconds(isGlowing);

        // set glow to false
        glowBool = false;

        // activate normal
        normal.SetActive(true);

        // deactivate glow
        glow.SetActive(false);

        Debug.Log("Glow ended");
    }
}
