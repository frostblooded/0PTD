using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisoningBullet : Bullet
{
    [HideInInspector]
    public float poisonDuration;
    [HideInInspector]
    public float poisonDamage;
    [HideInInspector]
    public float poisonDamageCooldown;

    public override void OnContact(Attackable target)
    {
        Effectable effectable = target.GetComponent<Effectable>();
        effectable.AddEffect(new PoisonEffect(target.gameObject, poisonDuration, poisonDamage, poisonDamageCooldown));
    }
}
