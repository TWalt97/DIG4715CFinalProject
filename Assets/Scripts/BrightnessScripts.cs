using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessScripts : MonoBehaviour
{
    public Slider slider;

    [Header("Point Light")]
    GameObject[] directionLight;
    [SerializeField]
    private NewLightController newLightController;

    void Start()
    {
        directionLight = GameObject.FindGameObjectsWithTag("light");
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject go in directionLight)
        {
            go.GetComponent<Light>().intensity = SaveValues.spookyPercent;
        }
    }
}
