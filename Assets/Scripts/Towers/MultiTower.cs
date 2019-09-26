using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTower : MonoBehaviour
{
    Shooting shooting;

    private void Start()
    {
        shooting = GetComponent<Shooting>();
    }

    private void Update()
    {
        if (shooting.IsReadyToAttack())
        {
            ShootAtAll();
        }
    }

    private void ShootAtAll() {
        var attackables = FindObjectsOfType<Attackable>();

        foreach(var attackable in attackables) {
            shooting.Shoot(attackable);
        }

        shooting.lastAttack = Time.time;
    }
}
