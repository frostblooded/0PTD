using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float attackRange;
    public float attackCooldown;

    float lastAttack;
    Attackable lockedOnTarget;
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
            SetLockedOnTarget();

            if(lockedOnTarget) {
                shooting.Shoot(lockedOnTarget);
                lastAttack = Time.time;
            }
        }
    }

    private void SetLockedOnTarget()
    {
        if(!lockedOnTarget || Vector3.Distance(transform.position, lockedOnTarget.transform.position) > attackRange)
        {
            lockedOnTarget = GetClosestAttackableInRange();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private Attackable GetClosestAttackableInRange()
    {
        Attackable closestAttackable = null;
        float closestAttackableDistance = Mathf.Infinity;
        Attackable[] attackables = FindObjectsOfType<Attackable>();

        foreach (var attackable in attackables)
        {
            float currentDist = Vector3.Distance(transform.position, attackable.transform.position);

            if (currentDist < closestAttackableDistance && currentDist <= attackRange)
            {
                closestAttackable = attackable;
                closestAttackableDistance = currentDist;
            }
        }

        return closestAttackable;
    }
}
