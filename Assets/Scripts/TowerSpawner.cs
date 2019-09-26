using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    public float spawnCooldownCoeficient;
    public float spawnCooldown;

    public GameObject normalTowerPrefab;
    public GameObject multiTowerPrefab;
    public GameObject poisoningTowerPrefab;

    public float normalTowerSpawnChance = 45f;
    public float multiTowerSpawnChance = 10f;
    public float poisoningTowerSpawnChance = 45f;

    float lastSpawnTime;

    private void Start()
    {
        lastSpawnTime = Mathf.NegativeInfinity;
        Maze maze = GameObject.Find("Globals").GetComponent<Maze>();
        spawnCooldown = Settings.Instance.towerDespawnDelay * spawnCooldownCoeficient / maze.path.Count;

        if(spawnCooldown > Settings.Instance.towerDespawnDelay) {
            spawnCooldown = Settings.Instance.towerDespawnDelay;
        }

        if(!Mathf.Approximately(normalTowerSpawnChance + multiTowerSpawnChance + poisoningTowerSpawnChance, 100)) {
            Debug.LogError("Tower spawn chances must be 100% when summed.");
        }
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
            randomUnoccupiedLanding.SpawnTower(GetRandomTowerPrefab());
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

    private GameObject GetRandomTowerPrefab() {
        float randomFloat = Random.Range(0, 101);
        var towers = GetTowersWithSpawnChances();
        float stackingChance = 0;

        foreach(var tower in towers) {
            if(stackingChance + tower.Key >= randomFloat) {
                return tower.Value;
            }

            stackingChance += tower.Key;
        }

        return towers[0].Value;
    }

    private List<KeyValuePair<float, GameObject>> GetTowersWithSpawnChances() {
        var res =  new List<KeyValuePair<float, GameObject>>() {
            new KeyValuePair<float, GameObject>(normalTowerSpawnChance, normalTowerPrefab),
            new KeyValuePair<float, GameObject>(multiTowerSpawnChance, multiTowerPrefab),
            new KeyValuePair<float, GameObject>(poisoningTowerSpawnChance, poisoningTowerPrefab),
        };

        res.Sort((x, y) => y.Key.CompareTo(x.Key));
        return res;
    }
}
