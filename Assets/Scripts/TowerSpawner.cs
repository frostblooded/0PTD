using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    public float spawnCooldown;

    Transform landingsHolder;
    float lastSpawnTime;

    private void Start()
    {
        landingsHolder = GameObject.Find("Landings").transform;
        lastSpawnTime = Mathf.NegativeInfinity;
    }

    private void Update()
    {
        if (lastSpawnTime + spawnCooldown <= Time.time)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        Landing randomUnoccupiedLanding = GetRandomUnoccupiedLanding();
        
        if(randomUnoccupiedLanding)
        {
            randomUnoccupiedLanding.SpawnTower();
            lastSpawnTime = Time.time;
        }
    }

    private Landing GetRandomUnoccupiedLanding()
    {
        List<Landing> unoccupiedLandings = GetUnoccupiedLandings();

        if(unoccupiedLandings.Count == 0)
        {
            return null;
        }

        return unoccupiedLandings[Random.Range(0, unoccupiedLandings.Count)];
    }

    private List<Landing> GetUnoccupiedLandings()
    {
        var result = new List<Landing>();

        foreach (Transform landingObject in landingsHolder)
        {
            Landing landing = landingObject.GetComponent<Landing>();

            if(!landing.IsOccupied())
            {
                result.Add(landing);
            }
        }

        return result;
    }
}
