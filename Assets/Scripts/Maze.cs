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

    [SerializeField]
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
        StartCoroutine("MakePath");
    }

    private IEnumerator MakePath()
    {
        int startingMazeUnitIndex = Random.Range(0, borderMazeUnits.Count);
        MazeUnit startingMazeUnit = borderMazeUnits[startingMazeUnitIndex];

        Stack<MazeUnit> stack = new Stack<MazeUnit>();
        stack.Push(startingMazeUnit);

        while(stack.Count > 0)
        {
            MazeUnit current = stack.Pop();
            current.gameObject.SetActive(false);

            foreach(var neighbour in current.RandomizedDirectNeighbours())
            {
                if(neighbour.CanBeDug())
                {
                    yield return new WaitForSeconds(0);

                    neighbour.gameObject.SetActive(false);
                    stack.Push(neighbour);
                }
            }
        }
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

        if(AreBorderIndexes(i, j))
        {
            borderMazeUnits.Add(mazeUnit);
        }

        SetNeighbouring(mazeUnit, i, j);
    }

    private void SetNeighbouring(MazeUnit mazeUnit, int i, int j)
    {
        if(i > 0)
        {
            matrix[i - 1][j].BecomeDirectNeighbours(mazeUnit);

            if(j > 0)
            {
                matrix[i - 1][j - 1].BecomeNeighbours(mazeUnit);
            }

            if(j < basePlatformZScale - 1)
            {
                matrix[i - 1][j + 1].BecomeNeighbours(mazeUnit);
            }
        }

        if(j > 0)
        {
            matrix[i][j - 1].BecomeDirectNeighbours(mazeUnit);
        }
    }

    private bool AreBorderIndexes(int i, int j)
    {
        return i == 0 || j == 0 || i == basePlatformXScale - 1 || j == basePlatformZScale - 1;
    }
}
