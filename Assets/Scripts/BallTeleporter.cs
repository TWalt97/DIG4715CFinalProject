using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTeleporter : MonoBehaviour
{
    public GameObject ball;
    public Transform startPos;
    // Start is called before the first frame update
    void Start()
    {
        Collider collider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>() != null)
        {
            other.transform.position = startPos.position;
        }
    }
}
