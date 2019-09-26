using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingBullet : Bullet
{
    [HideInInspector]
    public float damage;

    public override void OnContact(Attackable target)
    {
        target.Damage(damage);
    }
}
