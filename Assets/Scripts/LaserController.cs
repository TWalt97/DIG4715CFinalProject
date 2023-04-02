using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaserController : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject zoomCamera;
    public GameObject orientation;
    public GameObject crosshair;
    bool aiming = false;

    public float range;
    public ParticleSystem laserParticle;

    private PlayerControls playerControls;
    public LayerMask layerMask;
    Animator animator;

    private void Awake()
    {
        playerControls = new PlayerControls();
        animator = GetComponentInChildren<Animator>();
    }


    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void OnAim()
    {
        aiming = !aiming;
        if (aiming == true)
        {
            mainCamera.SetActive(false);
            zoomCamera.SetActive(true);
            crosshair.SetActive(true);
            orientation.transform.rotation = mainCamera.transform.rotation;
        }
        else
        {
            mainCamera.SetActive(true);
            zoomCamera.SetActive(false);
            crosshair.SetActive(false);
            aiming = false;
            mainCamera.transform.rotation = orientation.transform.rotation;
        }
    }

    void OnShoot()
    {
        if (aiming == true)
        {
            Invoke("Shoot", 0.5f);
            animator.SetBool("Shoot", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (aiming == true)
        {
            mainCamera.SetActive(false);
            zoomCamera.SetActive(true);
            crosshair.SetActive(true);
        }
        else
        {
            mainCamera.SetActive(true);
            zoomCamera.SetActive(false);
            crosshair.SetActive(false);
            aiming = false;
        }
    }

    void Shoot()
    {
        //laserParticle is actually just the laser, currently located as a child of the camera itself

        laserParticle.Play();
        RaycastHit hit;
        if (Physics.Raycast(laserParticle.transform.position, laserParticle.transform.forward, out hit, range, layerMask))
        {
            DestructibleObject destructibleObject = hit.transform.GetComponent<DestructibleObject>();
            if (destructibleObject != null)
            {
                destructibleObject.TakeDamage(1);
            }
        }
        animator.SetBool("Shoot", false);
    }
}
