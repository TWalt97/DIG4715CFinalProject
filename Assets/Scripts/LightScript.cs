using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public GameObject directionLight;
    // ability to be active for 5 seconds
    public float isLight = 10f;

    // cooldown
    public float cooldownTime = 10f;
    public float nextFireTime = 0;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFireTime)
        {
            // cooldown
            nextFireTime = Time.time + cooldownTime;

            // ability in use again
            StartCoroutine(Ability(isLight));
        }
    }

    IEnumerator Ability(float isLight)
    {
        Debug.Log("darkness started");
        // deactivate normal
        directionLight.SetActive(false);

        yield return new WaitForSeconds(isLight);

        // activate normal
        directionLight.SetActive(true);
        Debug.Log("darkness ended");
    }
}
