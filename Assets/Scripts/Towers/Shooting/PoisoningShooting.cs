using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisoningShooting : Shooting
{
    public float poisonDuration;
    public float poisonDamage;
    public float poisonDamageCooldown;
    public GameObject poisoningBulletPrefab;

    public override GameObject Shoot(Attackable target)
    {
        GameObject newBullet = base.Shoot(target);

        PoisoningBullet poisoningBullet = newBullet.GetComponent<PoisoningBullet>();
        poisoningBullet.poisonDuration = poisonDuration;
        poisoningBullet.poisonDamage = poisonDamage;
        poisoningBullet.poisonDamageCooldown = poisonDamageCooldown;

        return newBullet;
    }

    public override GameObject GetBulletPrefab()
    {
        return poisoningBulletPrefab;
    }

}
