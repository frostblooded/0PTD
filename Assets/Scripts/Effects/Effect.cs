using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect
{
    [HideInInspector]
    public float duration;

    public Effect(float newDuration)
    {
        duration = newDuration;
    }

    public virtual void Tick(GameObject target) {}
}
