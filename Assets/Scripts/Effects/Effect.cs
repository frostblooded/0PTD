﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect
{
    public float duration = 10;

    public virtual void Tick(GameObject target) {}
}
