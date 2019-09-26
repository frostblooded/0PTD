using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : Effect
{
    public float damage = 2;
    public float damageCooldown = 2;

    float lastAttack;

    public PoisonEffect()
    {
        lastAttack = Time.time;
    }

    public override void Tick(GameObject target)
    {
        if(lastAttack + damageCooldown <= Time.time)
        {
            Attackable attackable = target.GetComponent<Attackable>();
            attackable.Damage(damage);
            lastAttack = Time.time;
        }
    }
}
