using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public int minBaseXSize = 20;
    public int maxBaseXSize = 50;

    public int minBaseZSize = 20;
    public int maxBaseZSize = 50;

    public GameObject mazeUnitPrefab;
    public GameObject baseUnitPrefab;
    public GameObject enemiesGoalPrefab;
    public GameObject enemiesSpawnerPrefab;

    public LinkedList<MazeUnit> path;

    public int pathTries = 0;
    public int pathMaxTries = 1000;
    public float maxPathSize;
    public Vector3 baseSize;

    List<List<MazeUnit>> matrix;
    Transform platformsHolder;
    List<MazeUnit> startingMazeUnitCandidates;

    private void Start()
    {
        platformsHolder = GameObject.Find("Platforms").transform;
        matrix = new List<List<MazeUnit>>();
        startingMazeUnitCandidates = new List<MazeUnit>();
        path = new LinkedList<MazeUnit>();

        baseSize = new Vector3(Random.Range(minBaseXSize, maxBaseXSize), 1, Random.Range(minBaseZSize, maxBaseZSize));
        maxPathSize = baseSize.x * baseSize.z / 6;

        SpawnMazeUnits();
        MakePath();
        CleanupBase();

        var firstPathUnit = path.First.Value;
        var lastPathUnit = path.Last.Value;
        path.RemoveFirst();

        // Also setting the names manually as they have "(Clone)" at the end otherwise for some reason.
        var enemiesSpawnerObject = Instantiate(enemiesSpawnerPrefab, firstPathUnit.transform.position, Quaternion.identity);
        enemiesSpawnerObject.name = "Enemies Spawner";
        var enemiesGoalObject = Instantiate(enemiesGoalPrefab, lastPathUnit.transform.position, Quaternion.identity);
        enemiesGoalObject.name = "Enemies Goal";
    }

    private void CleanupBase()
    {
        // Maze units are not removed immediately, because that would change the state of the maze.
        // That's why we first add the maze units that need to be removed into a list while they are still being searched
        // and then deactivate them.
        List<MazeUnit> mazeUnitsToBeRemoved = new List<MazeUnit>();

        foreach(var mazeUnitList in matrix)
        {
            foreach(var mazeUnit in mazeUnitList)
            {
                if(mazeUnit.gameObject.activeSelf && !mazeUnit.HasInactiveNeighbours())
                {
                    mazeUnitsToBeRemoved.Add(mazeUnit);
                }
            }
        }

        foreach(var mazeUnit in mazeUnitsToBeRemoved)
        {
            mazeUnit.baseUnit.SetActive(false);
            mazeUnit.gameObject.SetActive(false);
        }
    }

    private void MakePath()
    {
        int startingMazeUnitIndex = Random.Range(0, startingMazeUnitCandidates.Count);
        MazeUnit startingMazeUnit = startingMazeUnitCandidates[startingMazeUnitIndex];
        MakePathHelper(startingMazeUnit);
    }

    private bool MakePathHelper(MazeUnit current, int length = 1)
    {
        pathTries++;
        current.gameObject.SetActive(false);
        path.AddLast(current);

        foreach(var dir in current.GetRandomizedAvailableDiggableDirections())
        {
            var neighbour = current.neighbours[dir];

            if(length >= maxPathSize)
            {
                return true;
            }

            if(neighbour.CanBeDugFrom(dir.GetOpposite()))
            {
                if (MakePathHelper(neighbour, length + 1))
                {
                    return true;
                }
                // Sometimes the algorithm can get stuck in a long recursion while trying to find a good lengthy path.
                // That is why we have this code here that forces the path to end after a number of tries.
                // We still check if the neighbour can be dug, because we don't want to dig for the end of the path from the
                // border or make a loop in the path.
                else if(pathTries >= pathMaxTries && neighbour.CanBeDugFrom(dir.GetOpposite()))
                {
                    path.AddLast(neighbour);
                    neighbour.gameObject.SetActive(false);
                    return true;
                }
            }
        }

        current.gameObject.SetActive(true);
        path.RemoveLast();
        return false;
    }

    private void SpawnMazeUnits()
    {
        float halfBaseXSize = baseSize.x / 2.0f;
        float halfBaseZSize = baseSize.z / 2.0f;

        float currentX = halfBaseXSize;
        float currentZ = halfBaseZSize;

        for(int i = 0; i < baseSize.x; i++)
        {
            matrix.Add(new List<MazeUnit>());

            for(int j = 0; j < baseSize.z; j++)
            {
                SpawnMazeUnit(i, j, currentX, currentZ);
                currentZ--;
            }

            currentX--;
            currentZ = halfBaseZSize;
        }
    }

    private void SpawnMazeUnit(int i, int j, float currentX, float currentZ)
    {
        GameObject mazeUnitObject = Instantiate(mazeUnitPrefab, platformsHolder);
        mazeUnitObject.transform.position = new Vector3(currentX, 1, currentZ);

        GameObject baseUnitObject = Instantiate(baseUnitPrefab, platformsHolder);
        baseUnitObject.transform.position = mazeUnitObject.transform.position + Vector3.down;

        MazeUnit mazeUnit = mazeUnitObject.GetComponent<MazeUnit>();
        mazeUnit.baseUnit = baseUnitObject;
        mazeUnit.x = i;
        mazeUnit.y = j;

        if(AreBorderIndexes(i, j))
        {
            mazeUnit.isBorder = true;
        }
        else
        {
            startingMazeUnitCandidates.Add(mazeUnit);
        }

        SetNeighbouring(mazeUnit, i, j);
        matrix[matrix.Count - 1].Add(mazeUnit);
    }

    private void SetNeighbouring(MazeUnit mazeUnit, int i, int j)
    {
        if(i > 0)
        {
            matrix[i - 1][j].BecomeNeighbours(MazeUnit.Direction.Top, mazeUnit);

            if(j > 0)
            {
                matrix[i - 1][j - 1].BecomeNeighbours(MazeUnit.Direction.TopLeft, mazeUnit);
            }

            if(j < baseSize.z - 1)
            {
                matrix[i - 1][j + 1].BecomeNeighbours(MazeUnit.Direction.TopRight, mazeUnit);
            }
        }

        if(j > 0)
        {
            matrix[i][j - 1].BecomeNeighbours(MazeUnit.Direction.Left, mazeUnit);
        }
    }

    // Returns if the maze unit at this position is on the border of the maze.
    private bool AreBorderIndexes(int i, int j)
    {
        return i == 0 || j == 0 || i == baseSize.x - 1 || j == baseSize.z - 1;
    }
}
