using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColosseumSpawnerController : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;
    // Start is called before the first frame update
    void OnEnable()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(false);
        transform.GetChild(5).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.colosseumTimer < 60)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        if (playerController.colosseumTimer < 55)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        if (playerController.colosseumTimer < 45)
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }
        if (playerController.colosseumTimer < 35)
        {
            transform.GetChild(3).gameObject.SetActive(true);
        }
        if (playerController.colosseumTimer < 25)
        {
            transform.GetChild(4).gameObject.SetActive(true);
        }
        if (playerController.colosseumTimer < 15)
        {
            transform.GetChild(5).gameObject.SetActive(true);
        }
    }
}
