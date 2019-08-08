using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Transform enemiesGoal;
    CastleDamaging castleDamaging;

    private void Start()
    {
        castleDamaging = GetComponent<CastleDamaging>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemiesGoal = GameObject.Find("EnemiesGoal").transform;

        navMeshAgent.SetDestination(enemiesGoal.position);
    }

    private void Update()
    {
        if(navMeshAgent.remainingDistance < 0.2)
        {
            castleDamaging.DamageCastle();
            Destroy(gameObject);
        }
    }
}
