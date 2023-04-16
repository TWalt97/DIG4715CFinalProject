using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetTracker : MonoBehaviour
{
    int targetsDestroyed;
    [SerializeField]
    private TextMeshPro targetTracker;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetsDestroyed = 5 - this.gameObject.transform.childCount;
        targetTracker.text = ("Skele-Rats Destroyed: " + targetsDestroyed + "/5");
    }
}
