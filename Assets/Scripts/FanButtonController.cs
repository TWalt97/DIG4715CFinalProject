using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanButtonController : MonoBehaviour
{
    [SerializeField]
    private GameObject fan;
    private GameObject particle;
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
            particle = this.transform.GetChild(0).gameObject;
            particle.GetComponent<ParticleSystem>().Play();
            AudioManager.Instance.PlaySFX("ButtonDestroy");
            DisableFan();
        }
    }

    private void DisableFan()
    {
        // AudioManager.Instance.PlaySFX("FanShutDown");

        fan.GetComponent<FanScript>().enabled = false;
        Destroy(gameObject, 1.5f);
    }
}
