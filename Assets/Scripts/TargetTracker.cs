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
        targetsDestroyed = 6 - this.gameObject.GetComponentsInChildren<Transform>().GetLength(0);
        targetTracker.text = ("Targets Destroyed: " + targetsDestroyed + "/5");
    }
}
