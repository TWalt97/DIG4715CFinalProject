using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject zoomCamera;
    public GameObject crosshair;
    bool aiming;

    public float range;
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
            crosshair.SetActive(true);
            aiming = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            mainCamera.SetActive(true);
            zoomCamera.SetActive(false);
            crosshair.SetActive(false);
            aiming = false;
        }

        if (aiming == true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        
    }
}
