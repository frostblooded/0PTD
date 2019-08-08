using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent navMesgAgent;

    Transform enemiesGoal;

    private void Start()
    {
        enemiesGoal = GameObject.Find("EnemiesGoal").transform;
        navMesgAgent.SetDestination(enemiesGoal.position);
    }
}
