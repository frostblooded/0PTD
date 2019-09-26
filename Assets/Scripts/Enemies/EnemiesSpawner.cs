using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    public float enemySpawnCooldown;
    public GameObject enemyPrefab;

    float lastSpawnTime;
    GameObject enemiesHolder;
    Maze maze;

    private void Start()
    {
        lastSpawnTime = Mathf.NegativeInfinity;
        enemiesHolder = GameObject.Find("Enemies");
        maze = GameObject.Find("Globals").GetComponent<Maze>();
    }

    private void Update()
    {
        if(lastSpawnTime + enemySpawnCooldown < Time.time)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        var enemyObject = Instantiate(enemyPrefab, transform.position, Quaternion.identity, enemiesHolder.transform);
        var enemyComponent = enemyObject.GetComponent<EnemyMovement>();
        enemyComponent.currentPathGoal = maze.path.First;

        lastSpawnTime = Time.time;
    }
}
