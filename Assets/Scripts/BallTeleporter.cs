using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTeleporter : MonoBehaviour
{
    public GameObject ball;
    public Transform startPos;
    int randomValue;

    // Start is called before the first frame update
    void Start()
    {
        Collider collider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>() != null)
        {
            other.transform.position = new Vector3(Random.Range(startPos.position.x, startPos.position.x), startPos.position.y, startPos.position.z);
        }
    }
}
