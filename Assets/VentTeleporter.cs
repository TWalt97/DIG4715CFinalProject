using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentTeleporter : MonoBehaviour
{
    [SerializeField]
    private Transform ventStartPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.position = ventStartPos.position;
        }
    }
}
