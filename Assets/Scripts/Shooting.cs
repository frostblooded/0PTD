using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float attackRange;
    public float attackCooldown;
    public int attackDamage;

    float lastAttack;

    private void Start()
    {
        lastAttack = Mathf.NegativeInfinity;
    }

    private void Update()
    {
        Attackable attackable = GetClosestAttackableInRange();

        if (attackable)
        {
            Debug.DrawLine(transform.position, GetClosestAttackableInRange().transform.position);

            if (lastAttack + attackCooldown < Time.time)
            {
                // If something is in range
                attackable.Damage(attackDamage);
                lastAttack = Time.time;
            }
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
            
            if(currentDist < closestAttackableDistance && currentDist < attackRange)
            {
                closestAttackable = attackable;
                closestAttackableDistance = currentDist;
            }
        }

        return closestAttackable;
    }
}
