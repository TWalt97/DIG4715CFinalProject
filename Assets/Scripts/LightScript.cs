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
    [SerializeField]
    private float lightTime = 10f;
    [SerializeField]
    private float darknessTime = 10f;
    private float resetTime;


    public float cooldownTime = 10f;
    public float nextFireTime = 0;



    void Awake()
    {
        directionLight = GameObject.FindGameObjectsWithTag("light");
        resetTime = 10f;

    }
    // Update is called once per frame
    void Update()
    {
        lightTime -= Time.deltaTime;
        if ((lightTime < 0 && lightTime > -0.1))
        {
            StartCoroutine(DisableLight(darknessTime));
        }
        //if (Time.time > nextFireTime)
        {
            // cooldown
            //nextFireTime = Time.time + cooldownTime;

            // ability in use again
            //StartCoroutine(Ability(isLight));
        }
    }

    IEnumerator DisableLight(float timeLightsAreOff)
    {
        foreach (GameObject light in directionLight)
        {
            light.SetActive(false);
        }

        yield return new WaitForSeconds(0.15f);

        foreach (GameObject light in directionLight)
        {
            light.SetActive(true);
        }

        yield return new WaitForSeconds(0.15f);

        foreach (GameObject light in directionLight)
        {
            light.SetActive(false);
        }

        yield return new WaitForSeconds(0.15f);

        foreach (GameObject light in directionLight)
        {
            light.SetActive(true);
        }

        yield return new WaitForSeconds(0.6f);

        foreach (GameObject light in directionLight)
        {
            light.SetActive(false);
        }

        yield return new WaitForSeconds(timeLightsAreOff);

        foreach (GameObject light in directionLight)
        {
            light.SetActive(true);
        }

        yield return new WaitForSeconds(0.15f);

        foreach (GameObject light in directionLight)
        {
            light.SetActive(false);
        }

        yield return new WaitForSeconds(0.15f);

        foreach (GameObject light in directionLight)
        {
            light.SetActive(true);
        }

        yield return new WaitForSeconds(0.15f);

        foreach (GameObject light in directionLight)
        {
            light.SetActive(false);
        }

        yield return new WaitForSeconds(0.6f);

        foreach (GameObject light in directionLight)
        {
            light.SetActive(true);
        }
        lightTime = resetTime;
    }

    IEnumerator Ability(float isLight)
    {
        Debug.Log("darkness started");
        // deactivate normal
        foreach (GameObject go in directionLight)
        {
            go.SetActive(false);
            Debug.Log("Lights off");
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
