using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject skeletonRatPrefab;
    [SerializeField]
    private Transform player;
    float timer = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 5f;
            GameObject ratSkeleton = GameObject.Instantiate(skeletonRatPrefab, this.transform.position, Quaternion.identity);
            ratSkeleton.GetComponent<EnemyController>().playerLocation = player;
        }
    }
}
