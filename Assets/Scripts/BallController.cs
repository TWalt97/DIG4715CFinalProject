using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody rb;
    public int speed;
    Vector3 ballDir = new Vector3(1, -0.75f, 0);
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Starting force of the ball
        rb.AddForce(ballDir * speed);
    }
}
