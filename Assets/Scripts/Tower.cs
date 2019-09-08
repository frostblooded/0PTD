using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    Attackable lockedOnTarget;
    Shooting shooting;
    RangedShooting rangedShooting;

    private void Start()
    {
        shooting = GetComponent<Shooting>();
        rangedShooting = GetComponent<RangedShooting>();
    }

    private void Update()
    {
        if (shooting.IsReadyToAttack())
        {
            SetLockedOnTarget();

            if(lockedOnTarget) {
                shooting.Shoot(lockedOnTarget);
                shooting.lastAttack = Time.time;
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
