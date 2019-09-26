using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public Attackable target;

    public abstract void OnContact(Attackable target);

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
            OnContact(target);
            Destroy(gameObject);
        }
    }
}
