using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Effectable : MonoBehaviour
{
    private List<KeyValuePair<Effect, float>> effects;

    private void Start()
    {
        effects = new List<KeyValuePair<Effect, float>>();
    }

    private void Update()
    {
        RemoveExpiredEffects();
        
        foreach(var pair in effects)
        {
            pair.Key.Tick();
        }
    }

    private void RemoveExpiredEffects()
    {
        var expiredEffects = effects.Where(pair => pair.Key.duration + pair.Value <= Time.time);
        effects = effects.Where(pair => !expiredEffects.Contains(pair)).ToList();

        foreach(var pair in expiredEffects)
        {
            pair.Key.OnEnd();
        }
    }

    public void AddEffect(Effect effect)
    {
        effects.Add(new KeyValuePair<Effect, float>(effect, Time.time));
        effect.OnStart();
    }
}
