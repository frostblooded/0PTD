﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Attackable target;
    public float damage;

    private void Update()
    {
        if (!target)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);;

        float distance = Vector3.Distance(transform.position, target.transform.position);

        if(Mathf.Approximately(distance, 0))
        {
            target.Damage(damage);
            Destroy(gameObject);
        }
    }
}
