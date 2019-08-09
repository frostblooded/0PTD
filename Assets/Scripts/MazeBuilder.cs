using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBuilder : MonoBehaviour
{
    private void Start()
    {
        int randomX = Random.Range(20, 50);
        int randomZ = Random.Range(20, 50);

        GameObject basePlatform = GameObject.CreatePrimitive(PrimitiveType.Cube);
        basePlatform.name = "Base Platform";

        Vector3 currentScale = basePlatform.transform.localScale;
        basePlatform.transform.localScale = new Vector3(randomX, currentScale.y, randomZ);

        Transform platformsHolder = GameObject.Find("Platforms").transform;

        float halfX = randomX / 2.0f;
        float halfZ = randomZ / 2.0f;

        for(float i = -halfX; i < halfX; i++)
        {
            for(float j = -halfZ; j < halfZ; j++)
            {
                GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                newCube.transform.position = new Vector3(i + 0.5f, 1, j + 0.5f);
                newCube.transform.parent = platformsHolder;
                newCube.isStatic = true;
            }
        }
    }
}
