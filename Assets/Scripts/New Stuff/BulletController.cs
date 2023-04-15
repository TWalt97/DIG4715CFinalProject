using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    private float speed = 50f;
    private float timeToDestroy = 3f;

    public Vector3 target { get; set;  }
    public bool hit { get; set;  }
    NewPlayerController newPlayerController;

    private void OnEnable() 
    {
        Destroy(gameObject, timeToDestroy);
        newPlayerController = GetComponentInParent<NewPlayerController>();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(!hit && Vector3.Distance(transform.position, target) < 0.01f)
        {
            Destroy(gameObject);
            newPlayerController.laserParticle.Stop();
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        Destroy(gameObject);
    }
}