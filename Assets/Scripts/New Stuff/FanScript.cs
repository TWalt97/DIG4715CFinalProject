using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{
    float speed;

    public float speedPush;

    float fanTimer = 10f;

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
        speed = fullSpeed;

        yield return new WaitForSeconds(fullSpeedTime);

        speed = normalSpeed;       
    }

    void PushBack()
    {
        player.GetComponent<Rigidbody>().AddForce(transform.forward * speedPush, ForceMode.Force);
    }
}
