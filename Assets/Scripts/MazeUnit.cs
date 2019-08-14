using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeUnit : MonoBehaviour
{
    public List<MazeUnit> neighbours;

    // These are the neighbours that aren't neighbouring diagonally
    public List<MazeUnit> directNeighbours;

    public List<MazeUnit> RandomizedDirectNeighbours()
    {
        List<MazeUnit> list = new List<MazeUnit>(directNeighbours);
        list.Shuffle();
        return list;
    }

    public void BecomeDirectNeighbours(MazeUnit other)
    {
        directNeighbours.Add(other);
        other.directNeighbours.Add(this);
    }

    public void BecomeNeighbours(MazeUnit other)
    {
        BecomeDirectNeighbours(other);
        neighbours.Add(other);
        other.neighbours.Add(this);
    }

    public bool CanBeDug()
    {
        return gameObject.activeSelf && GetInactiveNeighboursCount() <= 1;
    }

    private int GetInactiveNeighboursCount()
    {
        int inactiveNeighbours = 0;

        foreach(var neighbour in neighbours)
        {
            if (!neighbour.gameObject.activeSelf) inactiveNeighbours++;
        }

        return inactiveNeighbours;
    }
}
