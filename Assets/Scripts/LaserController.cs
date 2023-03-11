using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject zoomCamera;
    bool aiming;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            mainCamera.SetActive(false);
            zoomCamera.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            mainCamera.SetActive(true);
            mainCamera.SetActive(false);
        }
    }
}
