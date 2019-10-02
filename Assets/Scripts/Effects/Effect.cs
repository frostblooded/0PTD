using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect
{
    [HideInInspector]
    public float remainingDuration;

    public GameObject target;

    public Effect(GameObject newTarget, float newRemainingDuration)
    {
        remainingDuration = newRemainingDuration;
        target = newTarget;
    }

    public virtual void Tick() { }
    public virtual void OnStart() { }
    public virtual void OnEnd() { }
}
