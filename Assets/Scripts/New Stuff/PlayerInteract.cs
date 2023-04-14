using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerInteract : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerControls playerControls;
    private InputAction interactAction;
    [SerializeField]
    private float interactRange = 15f;
    PlayerController playerController;
    bool interacting = false;
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerInput = GetComponent<PlayerInput>();
        playerControls = new PlayerControls();

        interactAction = playerInput.actions["Interact"];
    }

    void OnEnable()
    {
        playerControls.Enable();
        interactAction.performed += _ => Interact();
    }

    private void OnDisable() 
    {
        playerControls.Disable();
    }

    private void Interact()
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out TutorialTeleporter tutorialTeleporter))
            {
                interacting = !interacting;
            }
        }
        if (interacting == true)
        {
            interacting = !interacting;
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
    void Update()
    {
        
    }

}
