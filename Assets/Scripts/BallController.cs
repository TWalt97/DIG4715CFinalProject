using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody rb;
    public int force;
    Vector3 ballDir = new Vector3(1, -0.75f, 0);
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(ballDir * force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
