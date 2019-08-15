using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public int minBasePlatformXScale = 20;
    public int maxBasePlatformXScale = 50;
    public int minBasePlatformYScale = 20;
    public int maxBasePlatformYScale = 50;

    public GameObject mazeUnitPrefab;
    public List<List<MazeUnit>> matrix;

    int basePlatformXScale;
    int basePlatformZScale;
    Transform platformsHolder;
    List<MazeUnit> borderMazeUnits;

    readonly float MAZE_UNIT_POSITION_OFFSET = 0.5f;

    private void Start()
    {
        platformsHolder = GameObject.Find("Platforms").transform;
        matrix = new List<List<MazeUnit>>();
        borderMazeUnits = new List<MazeUnit>();

        SpawnBasePlatform();
        SpawnMazeUnits();
        MakePath();
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
        Debug.Log("End pathing");
    }

    private bool MakePathHelper(MazeUnit current, int length = 0)
    {
        current.gameObject.SetActive(false);
        bool foundPath = false;

        foreach(var dir in current.GetRandomizedAvailableDiggableDirections())
        {
            var neighbour = current.neighbours[dir];

            if(neighbour.isBorder && length >= basePlatformXScale * basePlatformZScale / 6)
            {
                neighbour.gameObject.SetActive(false);
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

    private void SpawnBasePlatform()
    {
        basePlatformXScale = Random.Range(minBasePlatformXScale, maxBasePlatformXScale);
        basePlatformZScale = Random.Range(minBasePlatformYScale, minBasePlatformYScale);

        GameObject basePlatform = GameObject.CreatePrimitive(PrimitiveType.Cube);
        basePlatform.name = "Base Platform";
        basePlatform.transform.parent = platformsHolder;

        Vector3 currentScale = basePlatform.transform.localScale;
        basePlatform.transform.localScale = new Vector3(basePlatformXScale, currentScale.y, basePlatformZScale);
    }

    private void SpawnMazeUnits()
    {
        float halfBasePlatformXScale = basePlatformXScale / 2.0f;
        float halfBasePlatformZScale = basePlatformZScale / 2.0f;

        float currentX = halfBasePlatformXScale;
        float currentZ = halfBasePlatformZScale;

        for(int i = 0; i < basePlatformXScale; i++)
        {
            matrix.Add(new List<MazeUnit>());

            for(int j = 0; j < basePlatformZScale; j++)
            {
                SpawnMazeUnit(i, j, currentX, currentZ);
                currentZ--;
            }

            currentX--;
            currentZ = halfBasePlatformZScale;
        }
    }

    private void SpawnMazeUnit(int i, int j, float currentX, float currentZ)
    {
        GameObject mazeUnitObject = Instantiate(mazeUnitPrefab, transform);
        mazeUnitObject.transform.position = new Vector3(currentX - MAZE_UNIT_POSITION_OFFSET, 1, currentZ - MAZE_UNIT_POSITION_OFFSET);

        MazeUnit mazeUnit = mazeUnitObject.GetComponent<MazeUnit>();
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

            if(j < basePlatformZScale - 1)
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
        return i == 0 || j == 0 || i == basePlatformXScale - 1 || j == basePlatformZScale - 1;
    }
}
