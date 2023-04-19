using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLightController : MonoBehaviour
{
    GameObject[] directionLight;
    Light[] lightComponents;
    [SerializeField]
    float lightCooldownTimer = 15f;
    float resetTime;
    public float maxIntensity;
    private bool lightToggle;
    // Start is called before the first frame update
    void Start()
    {
        directionLight = GameObject.FindGameObjectsWithTag("light");
        lightComponents = new Light[directionLight.Length];
        for (int i = 0; i < directionLight.Length; i++)
        {
            lightComponents[i] = directionLight[i].GetComponent<Light>();
        }

        resetTime = lightCooldownTimer;

    }

    // Update is called once per frame
    void Update()
    {
        maxIntensity = SaveValues.spookyPercent;
        lightCooldownTimer -= Time.deltaTime;
        if (lightCooldownTimer < 0)
        {
            Debug.Log(lightToggle);
            lightToggle = !lightToggle;
            lightCooldownTimer = resetTime;
            if (lightToggle == true)
            {
                for (int i = 0; i < lightComponents.Length; i++)
                {
                    StartCoroutine(FadeLightSource.StartFade(lightComponents[i], 5f, 0));
                }
            }
            else if (lightToggle == false)
            {
                for (int i = 0; i < lightComponents.Length; i++)
                {
                    StartCoroutine(FadeLightSource.StartFade(lightComponents[i], 5f, maxIntensity));
                }
            }
        }

        
    }

    public static class FadeLightSource
    {
        public static IEnumerator StartFade(Light light, float duration, float targetIntensity)
        {
            float currentTime = 0;
            float start = light.intensity;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                light.intensity = Mathf.Lerp(start, targetIntensity, currentTime / duration);
                yield return null;
            }
            yield break;
        }
    }
}
