using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{   
    // Taken from https://stackoverflow.com/a/1262619/3659426
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    static readonly Dictionary<MazeUnit.Direction, MazeUnit.Direction> mazeUnitDirectionOpposites = new Dictionary<MazeUnit.Direction, MazeUnit.Direction>()
    {
        { MazeUnit.Direction.Top, MazeUnit.Direction.Bottom },
        { MazeUnit.Direction.TopRight, MazeUnit.Direction.BottomLeft },
        { MazeUnit.Direction.Right, MazeUnit.Direction.Left },
        { MazeUnit.Direction.BottomRight, MazeUnit.Direction.TopLeft },
        { MazeUnit.Direction.Bottom, MazeUnit.Direction.Top },
        { MazeUnit.Direction.BottomLeft, MazeUnit.Direction.TopRight },
        { MazeUnit.Direction.Left, MazeUnit.Direction.Right },
        { MazeUnit.Direction.TopLeft, MazeUnit.Direction.BottomRight }
    };

    public static MazeUnit.Direction GetOpposite(this MazeUnit.Direction direction)
    {
        return mazeUnitDirectionOpposites[direction];
    }
}
