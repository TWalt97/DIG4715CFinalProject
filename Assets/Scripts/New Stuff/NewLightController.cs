using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLightController : MonoBehaviour
{
    [SerializeField]
    float lightCooldownTimer = 5f;
    float resetTime;
    public Light directionLight;
    // Start is called before the first frame update
    void Start()
    {
        resetTime = lightCooldownTimer;
    }

    // Update is called once per frame
    void Update()
    {
        lightCooldownTimer -= Time.deltaTime;
        if (lightCooldownTimer <= 0)
        {
            if (directionLight.intensity == 1f)
            {
                StartCoroutine(FadeLightSource.StartFade(directionLight, 1f, 0.05f));
                lightCooldownTimer = resetTime;
            }
            if (directionLight.intensity == 0.05f)
            {
                StartCoroutine(FadeLightSource.StartFade(directionLight, 1f, 1f));
                lightCooldownTimer = resetTime; 
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
