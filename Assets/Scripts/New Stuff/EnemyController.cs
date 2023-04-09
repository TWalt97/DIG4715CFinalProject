using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform playerLocation;
    Animator animator;
    Rigidbody rb;
    public int health = 2;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent.enabled == true)
        {
            agent.destination = playerLocation.position;
        }   
    }

    /*private void OnCollisionEnter(Collision other) 
    {
        BulletController bulletController = other.gameObject.GetComponent<BulletController>();
        if (bulletController != null)
        {
            Debug.Log("Enemy hit!");
            TakeDamage(1);
        }
    }*/

    private void OnParticleCollision(GameObject other) 
    {
        if (other.tag == "Laser")
        {
            TakeDamage(1);
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.GetComponent<NewPlayerController>() != null)
        {
            other.gameObject.GetComponent<NewPlayerController>().health -= 1;
            Debug.Log(other.gameObject.GetComponent<NewPlayerController>().health);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            animator.SetBool("Death", true);
            agent.enabled = false;
            Invoke("Die", 1f);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
