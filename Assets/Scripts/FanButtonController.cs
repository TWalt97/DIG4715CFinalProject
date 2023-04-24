using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanButtonController : MonoBehaviour
{
    [SerializeField]
    private GameObject fan;
    private GameObject particle;
    private bool buttonDestroyed = false;
    // Start is called before the first frame update
    void OnEnable()
    {
        buttonDestroyed = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Laser" && buttonDestroyed == false)
        {
            buttonDestroyed = true;
            particle = this.transform.GetChild(0).gameObject;
            particle.GetComponent<ParticleSystem>().Play();
            
            DisableFan();
        }
    }

    private void DisableFan()
    {
        // AudioManager.Instance.PlaySFX("FanShutDown");
        AudioManager.Instance.PlaySFX("ButtonDestroy");

        fan.GetComponent<FanScript>().enabled = false;
        fan.GetComponent<AudioSource>().enabled = false;

        Invoke("SetInactive", 1.5f);
    }

    private void SetInactive()
    {
        this.gameObject.SetActive(false);
    }
}
