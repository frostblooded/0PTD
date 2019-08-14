using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeUnit : MonoBehaviour
{
    public List<MazeUnit> neighbours;

    // These are the neighbours that aren't neighbouring diagonally
    public List<MazeUnit> diagonalNeighbours;

    public bool isBorder = false;

    public List<MazeUnit> RandomizedNeighbours()
    {
        List<MazeUnit> list = new List<MazeUnit>(neighbours);
        list.Shuffle();
        return list;
    }

    public void BecomeDiagonalNeighbours(MazeUnit other)
    {
        BecomeNeighbours(other);
        diagonalNeighbours.Add(other);
        other.diagonalNeighbours.Add(this);
    }

    public void BecomeNeighbours(MazeUnit other)
    {
        neighbours.Add(other);
        other.neighbours.Add(this);
    }

    public bool CanBeDug()
    {
        return gameObject.activeSelf && GetInactiveDiagonalNeighboursCount() <= 1 && !isBorder;
    }

    private int GetInactiveDiagonalNeighboursCount()
    {
        int inactiveDiagonalNeighbours = 0;

        foreach(var diaognalNeighbour in diagonalNeighbours)
        {
            if (!diaognalNeighbour.gameObject.activeSelf) inactiveDiagonalNeighbours++;
        }

        return inactiveDiagonalNeighbours;
    }
}
