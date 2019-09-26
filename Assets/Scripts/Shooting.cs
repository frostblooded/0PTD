using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shooting : MonoBehaviour
{
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

    public virtual GameObject Shoot(Attackable target)
    {
        GameObject newBullet = Instantiate(GetBulletPrefab(), transform.position, Quaternion.identity, bulletHolder);

        Bullet bullet = newBullet.GetComponent<Bullet>();
        bullet.target = target;
        bullet.speed = bulletSpeed;

        return newBullet;
    }

    public bool IsReadyToAttack() {
        return lastAttack + attackCooldown < Time.time;
    }

    public abstract GameObject GetBulletPrefab();
}
