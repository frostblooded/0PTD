using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float attackCooldown;
    public float attackDamage;
    public float bulletSpeed;

    float lastAttack;

    private void Start()
    {
        lastAttack = Mathf.NegativeInfinity;
    }

    private void Update()
    {
        if (lastAttack + attackCooldown < Time.time)
        {
            Shoot();
        }
    }

    private void Shoot() {
        var attackables = FindObjectsOfType<Attackable>();

        foreach(var attackable in attackables) {
            ShootBullet(attackable);
        }

        lastAttack = Time.time;
    }

    private void ShootBullet(Attackable target)
    {
        GameObject bulletHolder = GameObject.Find("Bullets");
        GameObject newObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletHolder.transform);

        Bullet bullet = newObject.GetComponent<Bullet>();
        bullet.target = target;
        bullet.damage = attackDamage;
        bullet.speed = bulletSpeed;
    }
}
