using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour
{
    // player gameobjects
    public GameObject glow;
    public Material glowOn;
    public Material glowOff;

    // ability to be active for 5 seconds
    public float isGlowing = 10f;
    public bool glowBool = false;

    // cooldown
    public float cooldownTime = 10f;
    private float nextFireTime = 0;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFireTime)
        {
            if (Input.GetKey(KeyCode.E) && !glowBool)
            {
                // cooldown
                nextFireTime = Time.time + cooldownTime;

                // ability in use again
                StartCoroutine(Ability(isGlowing));
            }
        }
    }

    IEnumerator Ability(float isGlowing)
    {
        Debug.Log("Glow started");

        // set glow to true
        glowBool = true;

        // activate glow
        glow.SetActive(true);
        GetComponentInChildren<MeshRenderer>().material = glowOn;

        yield return new WaitForSeconds(isGlowing);

        // set glow to false
        glowBool = false;

        // deactivate glow
        glow.SetActive(false);
        GetComponentInChildren<MeshRenderer>().material = glowOff;

        Debug.Log("Glow ended");
    }
}
