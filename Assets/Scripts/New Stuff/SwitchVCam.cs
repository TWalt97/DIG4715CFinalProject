using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class SwitchVCam : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private int priorityBoostAmount = 10;
    private Canvas aimCanvas;
    [SerializeField]
    PlayerController playerController;

    private CinemachineVirtualCamera virtualCamera;
    private InputAction aimAction;
    [SerializeField]
    private HUD hud;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        aimAction = playerInput.actions["Aim"];
        aimCanvas = gameObject.GetComponentInChildren<Canvas>();
    }

    private void OnEnable()
    {
        aimAction.performed += _ => StartAim();
        aimAction.canceled += _ => CancelAim();
    }

    private void OnDisable()
    {
        aimAction.performed -= _ => StartAim();
        aimAction.canceled -= _ => CancelAim();
    }

    private void StartAim()
    {
        if (this != null)
        {
            virtualCamera.Priority += priorityBoostAmount;
            aimCanvas.enabled = true;
            playerController.aiming = true;
            hud.aiming = true;
        }   
    }

    private void CancelAim()
    {
        if (this != null)
        {
            virtualCamera.Priority -= priorityBoostAmount;
            aimCanvas.enabled = false;
            playerController.aiming = false;
            hud.aiming = false;
        }       
    }
}
