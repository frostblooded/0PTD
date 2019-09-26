using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Effectable : MonoBehaviour
{
    private Dictionary<Effect, float> effects;

    private void Start()
    {
        effects = new Dictionary<Effect, float>();
    }

    private void Update()
    {
        effects = GetNonexpiredEffects(effects);
        
        foreach(var effect in effects.Keys)
        {
            effect.Tick(gameObject);
        }
    }

    private Dictionary<Effect, float> GetNonexpiredEffects(Dictionary<Effect, float> effects)
    {
        return effects.Where(pair => pair.Key.duration + pair.Value > Time.time).ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    public void AddEffect(Effect effect)
    {
        effects.Add(effect, Time.time);
    }
}
