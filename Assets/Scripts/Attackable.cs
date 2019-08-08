using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour
{
    public float health;

    public void Damage(float damage)
    {
        health -= damage;

        if(health <= 0.0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
