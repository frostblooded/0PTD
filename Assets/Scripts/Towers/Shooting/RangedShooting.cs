using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedShooting : MonoBehaviour
{
    public float attackRange;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public Attackable GetClosestAttackableInRange()
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
