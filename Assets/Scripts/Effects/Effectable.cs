using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Effectable : MonoBehaviour
{
    private List<Effect> effects;

    private void Start()
    {
        effects = new List<Effect>();
    }

    private void Update()
    {
        ReduceEffectsRemainingDuration();
        RemoveExpiredEffects();
        
        foreach(var effect in effects)
        {
            effect.Tick();
        }
    }

    private void ReduceEffectsRemainingDuration()
    {
        foreach(var effect in effects)
        {
            effect.remainingDuration -= Time.deltaTime;
        }
    }

    private void RemoveExpiredEffects()
    {
        var expiredEffects = effects.Where(effect => effect.remainingDuration <= 0);
        effects = effects.Where(effect => !expiredEffects.Contains(effect)).ToList();

        foreach(var effect in expiredEffects)
        {
            effect.OnEnd();
        }
    }

    public void AddEffect(Effect effect)
    {
        Effect alreadyAddedEffect = GetEffectOfType(effect.GetType());


        if(alreadyAddedEffect != null)
        {
            alreadyAddedEffect.remainingDuration = effect.remainingDuration;
        }
        else
        {
            effects.Add(effect);
            effect.OnStart();
        }
    }

    public Effect GetEffectOfType(Type type) {
        foreach(var effect in effects)
        {
            if(effect.GetType() == type)
            {
                return effect;
            }
        }

        return null;
    }
}
