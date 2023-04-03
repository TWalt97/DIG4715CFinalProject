using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
  
    public float health = 5f;
    public ParticleSystem destroyEffect; 
   

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            destroyEffect.Play();
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
