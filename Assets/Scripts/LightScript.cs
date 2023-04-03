using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    [Header("Point Light")]
    GameObject[] directionLight;

    [Header("Active Light Time")]
    public float isLight = 10f;

    [Header("Cooldown")]
    public float cooldownTime = 10f;
    public float nextFireTime = 0;

    void Start()
    {
        directionLight = GameObject.FindGameObjectsWithTag("light");
    }
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
        foreach (GameObject go in directionLight)
        {
            go.SetActive(false);
        }
        //directionLight.SetActive(false);

        yield return new WaitForSeconds(isLight);

        // activate normal
        foreach (GameObject go in directionLight)
        {
            go.SetActive(true);
        }
        //directionLight.SetActive(true);
        Debug.Log("darkness ended");
    }
}
