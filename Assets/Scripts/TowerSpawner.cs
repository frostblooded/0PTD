using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    public float spawnCooldownCoeficient;
    public float spawnCooldown;

    float lastSpawnTime;

    private void Start()
    {
        lastSpawnTime = Mathf.NegativeInfinity;
        Maze maze = GameObject.Find("Maze").GetComponent<Maze>();
        spawnCooldown = Settings.Instance.normalTowerDespawnDelay * spawnCooldownCoeficient / maze.path.Count;
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
        Landing[] landings = FindObjectsOfType<Landing>();
        var result = new List<Landing>();

        foreach (Landing landing in landings)
        {
            if(!landing.IsOccupied())
            {
                result.Add(landing);
            }
        }

        return result;
    }
}
