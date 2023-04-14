using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{
    float speed;

    [Header("Full Speed")]
    public float fullSpeedTime;
    public float fullSpeed;

    [Header("Normal Speed")]
    public float normalTime;
    public float normalSpeed;
    float nextFireTime = 0;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFireTime)
        {
            // cooldown
            nextFireTime = Time.time + normalTime;

            // ability in use again
            StartCoroutine(FanTime(fullSpeedTime));
        }

        transform.Rotate(0, 0, speed, Space.Self);
    }

    IEnumerator FanTime(float fullSpeedTime)
    {
        Debug.Log("normal speed started");

        speed = normalSpeed;

        yield return new WaitForSeconds(fullSpeedTime);

        Debug.Log("normal speed ended");

        speed = fullSpeed;        
    }
}
