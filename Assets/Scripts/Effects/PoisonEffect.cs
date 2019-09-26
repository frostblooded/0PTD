using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : Effect
{
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float damageCooldown;

    float lastAttack;

    public PoisonEffect(float newDuration, float newDamage, float newDamageCooldown)
        : base(newDuration)
    {
        damage = newDamage;
        damageCooldown = newDamageCooldown;
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
