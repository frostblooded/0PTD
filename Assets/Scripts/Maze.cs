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
    public GameObject basePlatformPrefab;

    List<List<MazeUnit>> matrix;

    public Vector3 baseSize;

    Transform platformsHolder;
    List<MazeUnit> borderMazeUnits;

    public int pathTries = 0;
    public int pathLength = 0;

    public int pathMaxTries = 1000;
    readonly float MAZE_UNIT_POSITION_OFFSET = 0.5f;

    private void Start()
    {
        platformsHolder = GameObject.Find("Platforms").transform;
        matrix = new List<List<MazeUnit>>();
        borderMazeUnits = new List<MazeUnit>();

        baseSize = new Vector3(Random.Range(minBaseXSize, maxBaseXSize), 1, Random.Range(minBaseZSize, maxBaseZSize));

        SpawnMazeUnits();
        MakePath();
        CleanupBase();
    }

    private void CleanupBase()
    {
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
        int startingBorderMazeUnitIndex = Random.Range(0, borderMazeUnits.Count);
        MazeUnit startingBorderMazeUnit = borderMazeUnits[startingBorderMazeUnitIndex];
        startingBorderMazeUnit.gameObject.SetActive(false);

        MazeUnit startingMazeUnit = null;

        foreach(var neighbour in startingBorderMazeUnit.GetDiggableNeighbours())
        {
            if(!neighbour.isBorder)
            {
                startingMazeUnit = neighbour;
                break;
            }
        }

        MakePathHelper(startingMazeUnit);
    }

    private bool MakePathHelper(MazeUnit current, int length = 0)
    {
        pathTries++;
        current.gameObject.SetActive(false);
        bool foundPath = false;

        foreach(var dir in current.GetRandomizedAvailableDiggableDirections())
        {
            var neighbour = current.neighbours[dir];

            if(pathTries >= pathMaxTries || (neighbour.isBorder && length >= baseSize.x * baseSize.z / 6))
            {
                neighbour.gameObject.SetActive(false);
                pathLength = length;
                return true;
            }

            if(neighbour.CanBeDugFrom(dir.GetOpposite()))
            {
                foundPath = MakePathHelper(neighbour, length + 1);
                if (foundPath) return true;
            }
        }

        current.gameObject.SetActive(true);
        return foundPath;
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
        GameObject mazeUnitObject = Instantiate(mazeUnitPrefab, transform);
        mazeUnitObject.transform.position = new Vector3(currentX - MAZE_UNIT_POSITION_OFFSET, 1, currentZ - MAZE_UNIT_POSITION_OFFSET);

        GameObject baseUnitObject = Instantiate(baseUnitPrefab, platformsHolder);
        baseUnitObject.transform.position = mazeUnitObject.transform.position + Vector3.down;

        MazeUnit mazeUnit = mazeUnitObject.GetComponent<MazeUnit>();
        mazeUnit.baseUnit = baseUnitObject;
        matrix[matrix.Count - 1].Add(mazeUnit);
        mazeUnit.x = i;
        mazeUnit.y = j;

        if(AreBorderIndexes(i, j))
        {
            borderMazeUnits.Add(mazeUnit);
            mazeUnit.isBorder = true;
        }

        SetNeighbouring(mazeUnit, i, j);
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

    private bool AreBorderIndexes(int i, int j)
    {
        return i == 0 || j == 0 || i == baseSize.x - 1 || j == baseSize.z - 1;
    }
}
