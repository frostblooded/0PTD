using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisoningShooting : Shooting
{
    public GameObject poisoningBulletPrefab;

    public override GameObject Shoot(Attackable target)
    {
        GameObject newBullet = base.Shoot(target);

        PoisoningBullet poisoningBullet = newBullet.GetComponent<PoisoningBullet>();

        return newBullet;
    }

    public override GameObject GetBulletPrefab()
    {
        return poisoningBulletPrefab;
    }

}
