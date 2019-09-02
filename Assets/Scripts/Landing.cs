using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landing : MonoBehaviour
{
    float towerSpawnTime;
    GameObject spawnedTower;

    private void Update()
    {
        if(IsOccupied() && towerSpawnTime + Settings.Instance.towerDespawnDelay < Time.time)
        {
            DespawnTower();
        }
    }

    public void SpawnTower(GameObject prefab)
    {
        spawnedTower = Instantiate(prefab, transform);
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
