using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class DestructibleObject : MonoBehaviour
{
    public ParticleSystem smokeEffect;

    public float health = 5f;
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Instantiate(smokeEffect, transform.position, Quaternion.identity);
            smokeEffect.Play();
            Die();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Laser")
        {
            TakeDamage(1);
            other.GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
