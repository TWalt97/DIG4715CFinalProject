using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{

    public static DontDestroy Instance;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("multiple dont destroys");
            Destroy(this.gameObject);
        }

        Instance = this;

        DontDestroyOnLoad(this.gameObject);
    }
}
