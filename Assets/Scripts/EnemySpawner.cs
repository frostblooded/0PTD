using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float enemySpawnCooldown;
    public GameObject enemyPrefab;

    float lastSpawnTime;
    GameObject enemiesHolder;

    private void Start()
    {
        lastSpawnTime = Mathf.NegativeInfinity;
        enemiesHolder = GameObject.Find("Enemies");
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
        Instantiate(enemyPrefab, transform.position, Quaternion.identity, enemiesHolder.transform);
        lastSpawnTime = Time.time;
    }
}
