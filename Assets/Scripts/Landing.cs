using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landing : MonoBehaviour
{
    public GameObject towerPrefab;
    public float towerDespawnDelay;

    float towerSpawnTime;
    GameObject spawnedTower;

    private void Update()
    {
        if(IsOccupied() && towerSpawnTime + towerDespawnDelay < Time.time)
        {
            DespawnTower();
        }
    }

    public void SpawnTower()
    {
        spawnedTower = Instantiate(towerPrefab, transform);
        towerSpawnTime = Time.time;
    }

    private void DespawnTower()
    {
        Destroy(spawnedTower);
    }

    public bool IsOccupied()
    {
        return spawnedTower != null;
    }
}
