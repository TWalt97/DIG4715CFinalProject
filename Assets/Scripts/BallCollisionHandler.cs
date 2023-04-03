using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionHandler : MonoBehaviour
{
    private void Update() 
    {
        BallController ballController = GetComponentInParent<BallController>();
        transform.position = ballController.transform.position;
    }
    private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerController>() != null)
            {
                //Death goes here
                Debug.Log("You dead");
                other.GetComponent<PlayerController>().dead = true;
        }
        }
}
