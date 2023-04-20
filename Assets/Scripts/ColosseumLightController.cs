using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColosseumLightController : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;
    private GameObject colosseumLight;
    private Light cLight;
    bool coroutineActive = false;
    // Start is called before the first frame update
    void Start()
    {
        colosseumLight = GameObject.FindWithTag("ColosseumLight");
        cLight = colosseumLight.GetComponent<Light>();

    }

    // Update is called once per frame
    void Update()
    {
        if (((playerController.colosseumTimer > 55 && playerController.colosseumTimer < 60) || playerController.colosseumTimer < 0) && coroutineActive == true)
        {
            coroutineActive = false;
            StopAllCoroutines();
            cLight.intensity = 30000;
            Debug.Log("Coroutine stopped, intensity set to 30,000");
        }
        if (((playerController.colosseumTimer < 55) && (playerController.colosseumTimer > 0)) && coroutineActive == false)
        {
            StartCoroutine(FadeLightSource.StartFade(cLight, 60f, 0));
            coroutineActive = true;
            Debug.Log("Coroutine started");
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
