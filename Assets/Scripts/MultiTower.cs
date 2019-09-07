using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTower : MonoBehaviour
{
    public float attackCooldown;

    float lastAttack;
    Shooting shooting;

    private void Start()
    {
        lastAttack = Mathf.NegativeInfinity;
        shooting = GetComponent<Shooting>();
    }

    private void Update()
    {
        if (lastAttack + attackCooldown < Time.time)
        {
            ShootAtAll();
        }
    }

    private void ShootAtAll() {
        var attackables = FindObjectsOfType<Attackable>();

        foreach(var attackable in attackables) {
            shooting.Shoot(attackable);
        }

        lastAttack = Time.time;
    }
}
