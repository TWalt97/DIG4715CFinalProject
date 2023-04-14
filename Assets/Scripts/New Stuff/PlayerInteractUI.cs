using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField]
    private GameObject interactUIGameObject;
    [SerializeField] 
    PlayerController playerController;
    [SerializeField]
    private PlayerInteract playerInteract;


    void Update()
    {
        if (playerInteract.GetInteractableObject() != null)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        
    }

    private void Hide()
    {

    }
}
