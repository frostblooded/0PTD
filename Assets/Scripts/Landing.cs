using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landing : MonoBehaviour
{
    public GameObject towerPrefab;
    public bool occupied;

    public void SpawnTower()
    {
        Instantiate(towerPrefab, transform);
        occupied = true;
    }
}
