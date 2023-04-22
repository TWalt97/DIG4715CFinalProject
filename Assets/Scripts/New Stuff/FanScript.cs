using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{
    private float speed;

    private float speedPush;
    [SerializeField]
    private float maxSpeedPush;

    private float fanTimer = 20f;

    private FieldOfView fieldOfView;

    public GameObject player;
    private Vector3 pushDirection;

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
            fanTimer = 20f;
        }

        transform.Rotate(0, 0, speed, Space.Self);

        if (fieldOfView != null)
        {
            if (fieldOfView.canSeePlayer == true)
            {
                //Push Player back
                PushBack();
            }
        }
        
    }

    IEnumerator FanTime(float fullSpeedTime)
    {
        //speed = fullSpeed;
        StartCoroutine(ChangeFanSpeed.StartFade(this.gameObject.GetComponent<FanScript>(), 2f, fullSpeed, maxSpeedPush));

        yield return new WaitForSeconds(fullSpeedTime);

        //speed = normalSpeed;
        StartCoroutine(ChangeFanSpeed.StartFade(this.gameObject.GetComponent<FanScript>(), 2f, normalSpeed, 0));      
    }

    void PushBack()
    {
        pushDirection = new Vector3(fieldOfView.directionToTarget.x, 0, fieldOfView.directionToTarget.z).normalized;
        player.GetComponent<Rigidbody>().AddForce(pushDirection * speedPush, ForceMode.Force);
    }

    public static class ChangeFanSpeed
    {
        public static IEnumerator StartFade(FanScript fanScript, float duration, float targetSpeed, float targetPushSpeed)
        {
            float currentTime = 0;
            float start = fanScript.speed;
            float pushStart = fanScript.speedPush;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                fanScript.speed = Mathf.Lerp(start, targetSpeed, currentTime / duration);
                fanScript.speedPush = Mathf.Lerp(pushStart, targetPushSpeed, currentTime / duration);
                yield return null;
            }
            yield break;
        }
    }
}
