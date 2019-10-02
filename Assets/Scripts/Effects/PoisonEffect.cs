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
    Material poisonedMaterial;
    Material previousMaterial;
    MeshRenderer targetMeshRenderer;

    public PoisonEffect(GameObject newTarget, float newRemainingDuration, float newDamage, float newDamageCooldown)
        : base(newTarget, newRemainingDuration)
    {
        damage = newDamage;
        damageCooldown = newDamageCooldown;
        lastAttack = Time.time;
        targetMeshRenderer = target.GetComponent<MeshRenderer>();
    }

    public override void Tick()
    {
        base.Tick();

        if(lastAttack + damageCooldown <= Time.time)
        {
            Attackable attackable = target.GetComponent<Attackable>();
            attackable.Damage(damage);
            lastAttack = Time.time;
        }
    }

    public override void OnStart()
    {
        base.OnStart();
        poisonedMaterial =  Resources.Load("Materials/PoisonedMaterial", typeof(Material)) as Material;
        previousMaterial = targetMeshRenderer.material;
        targetMeshRenderer.material = poisonedMaterial;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        targetMeshRenderer.material = previousMaterial;
    }
}
