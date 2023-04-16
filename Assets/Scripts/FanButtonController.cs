using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanButtonController : MonoBehaviour
{
    [SerializeField]
    private GameObject fan;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Laser")
        {
            DisableFan();
        }
    }

    private void DisableFan()
    {
        fan.GetComponent<FanScript>().enabled = false;
        Destroy(gameObject, 0.5f);
    }
}
