using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{
    float speed;

    public float speedPush;

    float fanTimer = 15f;

    FieldOfView fieldOfView;

    public GameObject player;

    [Header("Full Speed")]
    public float fullSpeedTime;
    public float fullSpeed;

    [Header("Normal Speed")]
    public float normalSpeed;

    // private void Awake()
    // {
    //     fieldOfView = GetComponent<FieldOfView>();
    //     // characterController = GetComponent<CharacterController>();
    // }

    void Start()
    {
        fieldOfView = GetComponent<FieldOfView>();
        speed = normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        fanTimer -= Time.deltaTime;
        if (fanTimer <= 0)
        {
            StartCoroutine(FanTime(fullSpeedTime));
            fanTimer = 10f;
        }

        transform.Rotate(0, 0, speed, Space.Self);

        if ((speed == fullSpeed) && (fieldOfView.canSeePlayer == true))
        {
            //Push Player back
            PushBack();
        }
    }

    IEnumerator FanTime(float fullSpeedTime)
    {
        //speed = fullSpeed;
        StartCoroutine(ChangeFanSpeed.StartFade(this.gameObject.GetComponent<FanScript>(), 2f, fullSpeed));

        yield return new WaitForSeconds(fullSpeedTime);

        //speed = normalSpeed;
        StartCoroutine(ChangeFanSpeed.StartFade(this.gameObject.GetComponent<FanScript>(), 2f, normalSpeed));      
    }

    void PushBack()
    {
        player.GetComponent<Rigidbody>().AddForce(transform.forward * speedPush, ForceMode.Force);
    }

    public static class ChangeFanSpeed
    {
        public static IEnumerator StartFade(FanScript fanScript, float duration, float targetSpeed)
        {
            float currentTime = 0;
            float start = fanScript.speed;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                fanScript.speed = Mathf.Lerp(start, targetSpeed, currentTime / duration);
                yield return null;
            }
            yield break;
        }
    }
}
