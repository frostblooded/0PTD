using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public LinkedListNode<MazeUnit> currentPathGoal;

    Transform enemiesGoal;
    CastleDamaging castleDamaging;

    public float speed;

    private void Start()
    {
        castleDamaging = GetComponent<CastleDamaging>();
        enemiesGoal = GameObject.Find("Enemies Goal").transform;
    }

    private void Update()
    {
        var newPosition = Vector3.MoveTowards(transform.position, currentPathGoal.Value.transform.position, speed * Time.deltaTime);
        transform.position = newPosition;

        if(Mathf.Approximately(Vector3.Distance(transform.position, enemiesGoal.position), 0))
        {
            castleDamaging.DamageCastle();
            Destroy(gameObject);
        }
        else if(Mathf.Approximately(Vector3.Distance(transform.position, currentPathGoal.Value.transform.position), 0))
        {
            currentPathGoal = currentPathGoal.Next;
        }
    }
}
