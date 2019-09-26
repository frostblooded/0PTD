using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisoningBullet : Bullet
{
    public override void OnContact(Attackable target)
    {
        Effectable effectable = target.GetComponent<Effectable>();
        effectable.AddEffect(new PoisonEffect());
    }
}
