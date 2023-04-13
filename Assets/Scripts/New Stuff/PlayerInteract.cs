using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField]
    private float interactRange = 2f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out TutorialTeleporter tutorialTeleporter))
                {
                    Debug.Log("Interacted with TV");
                }
            }
        }
    }

    public TutorialTeleporter GetInteractableObject()
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out TutorialTeleporter tutorialTeleporter))
            {
                return tutorialTeleporter;
            }
        }
        return null;
    }
}
