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

    private void OnCollisionEnter(Collision other)
    {
        BulletController bulletController = other.gameObject.GetComponent<BulletController>();
        if (bulletController != null)
        {
            TakeDamage(1);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
