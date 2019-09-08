using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float attackDamage;
    public float bulletSpeed;
    public float attackCooldown;
    [HideInInspector]
    public float lastAttack;

    Transform bulletHolder;

    private void Start() {
        bulletHolder = GameObject.Find("Bullets").transform;
        lastAttack = Mathf.NegativeInfinity;
    }

    public void Shoot(Attackable target)
    {
        GameObject newObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletHolder);

        Bullet bullet = newObject.GetComponent<Bullet>();
        bullet.target = target;
        bullet.damage = attackDamage;
        bullet.speed = bulletSpeed;
    }

    public bool IsReadyToAttack() {
        return lastAttack + attackCooldown < Time.time;
    }
}
