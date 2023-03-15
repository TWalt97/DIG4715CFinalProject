using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject zoomCamera;
    public GameObject orientation;
    public GameObject crosshair;
    bool aiming;

    public float range;
    public ParticleSystem laserParticle;
    // Start is called before the first frame update
    void Start()
    {
        //This might not be the best place to hide and lock the cursor, so it can be moved to any other script.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Changes the camera to the "zoomed camera" and makes the crosshair UI element active
        //Likely a cleaner way to do this
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            mainCamera.SetActive(false);
            zoomCamera.SetActive(true);
            crosshair.SetActive(true);
            aiming = true;
            orientation.transform.rotation = mainCamera.transform.rotation;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            mainCamera.SetActive(true);
            zoomCamera.SetActive(false);
            crosshair.SetActive(false);
            aiming = false;
            mainCamera.transform.rotation = orientation.transform.rotation;
        }

        //Requires the player to press LMB while aiming
        if (aiming == true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        //laserParticle is actually just the laser, currently located as a child of the camera itself
        laserParticle.Play();
        RaycastHit hit;
        if (Physics.Raycast(zoomCamera.transform.position, zoomCamera.transform.forward, out hit, range))
        {
            DestructibleObject destructibleObject = hit.transform.GetComponent<DestructibleObject>();
            if (destructibleObject != null)
            {
                destructibleObject.TakeDamage(1);
            }
        }
    }
}
