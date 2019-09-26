using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingShooting : Shooting
{
    public float attackDamage;
    public GameObject damagingBulletPrefab;

    public override GameObject Shoot(Attackable target)
    {
        GameObject newBullet = base.Shoot(target);

        DamagingBullet damagingBullet = newBullet.GetComponent<DamagingBullet>();
        damagingBullet.damage = attackDamage;

        return newBullet;
    }

    public override GameObject GetBulletPrefab()
    {
        return damagingBulletPrefab;
    }
}
