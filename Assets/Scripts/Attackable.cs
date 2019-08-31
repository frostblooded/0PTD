using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attackable : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public Image healthBar;

    private void Start() {
        currentHealth = maxHealth;
        DrawHealthBar();
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        DrawHealthBar();

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void DrawHealthBar() {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
