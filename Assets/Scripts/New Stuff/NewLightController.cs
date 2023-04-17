using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLightController : MonoBehaviour
{
    GameObject[] directionLight;
    [SerializeField]
    float lightCooldownTimer = 5f;
    float resetTime;
    // Start is called before the first frame update
    void Start()
    {
        directionLight = GameObject.FindGameObjectsWithTag("light");
        resetTime = lightCooldownTimer;
    }

    // Update is called once per frame
    void Update()
    {

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
