using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject skeletonRatPrefab;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private AudioClip skeleDeathClip;
    float timer = 5f;
    bool spawnActivated;
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
                timer = Random.Range(2, 6);
                GameObject ratSkeleton = GameObject.Instantiate(skeletonRatPrefab, this.transform.position, Quaternion.identity);
                ratSkeleton.GetComponent<EnemyController>().playerLocation = player;
                ratSkeleton.GetComponent<EnemyController>().deathClip = skeleDeathClip;
        }  
    }
}
