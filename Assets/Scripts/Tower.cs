using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float attackCooldown;

    float lastAttack;
    Attackable lockedOnTarget;
    Shooting shooting;
    RangedShooting rangedShooting;

    private void Start()
    {
        lastAttack = Mathf.NegativeInfinity;
        shooting = GetComponent<Shooting>();
        rangedShooting = GetComponent<RangedShooting>();
    }

    private void Update()
    {
        if (lastAttack + attackCooldown < Time.time)
        {
            SetLockedOnTarget();

            if(lockedOnTarget) {
                shooting.Shoot(lockedOnTarget);
                lastAttack = Time.time;
            }
        }
    }

    private void SetLockedOnTarget()
    {
        // If there is no locked on target or it has gone out of range, pick a new one
        if(!lockedOnTarget || Vector3.Distance(transform.position, lockedOnTarget.transform.position) > rangedShooting.attackRange)
        {
            lockedOnTarget = rangedShooting.GetClosestAttackableInRange();
        }
    }
}
