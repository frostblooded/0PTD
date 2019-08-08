using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float attackRange;
    public float attackCooldown;
    public float attackDamage;
    public float bulletSpeed;

    float lastAttack;
    Attackable lockedOnTarget;

    private void Start()
    {
        lastAttack = Mathf.NegativeInfinity;
    }

    private void Update()
    {
        if (lastAttack + attackCooldown < Time.time)
        {
            ShootBullet();
        }
    }

    private void ShootBullet()
    {
        SetLockedOnTarget();

        if (lockedOnTarget)
        {
            GameObject bulletHolder = GameObject.Find("Bullets");
            GameObject newObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletHolder.transform);

            Bullet bullet = newObject.GetComponent<Bullet>();
            bullet.target = lockedOnTarget;
            bullet.damage = attackDamage;
            bullet.speed = bulletSpeed;

            lastAttack = Time.time;
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
