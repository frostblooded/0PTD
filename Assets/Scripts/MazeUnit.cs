using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeUnit : MonoBehaviour
{
    public Dictionary<Direction, MazeUnit> neighbours = new Dictionary<Direction, MazeUnit>();

    public bool isBorder = false;

    public int x, y;

    public GameObject baseUnit;

    public readonly List<Direction> diggableDirections = new List<Direction>()
    {
        Direction.Top,
        Direction.Right,
        Direction.Bottom,
        Direction.Left
    };

    // This maps the direction FROM which the digging is being done to the neighbours that need to be active for the current
    // maze unit to be diggable.
    public readonly Dictionary<Direction, List<Direction>> diggableDirectionsCheckGroups = new Dictionary<Direction, List<Direction>>()
    {
        { Direction.Top, new List<Direction>() { Direction.Right, Direction.BottomRight, Direction.Bottom, Direction.BottomLeft, Direction.Left } },
        { Direction.Right, new List<Direction>() { Direction.Top, Direction.Bottom, Direction.BottomLeft, Direction.Left, Direction.TopLeft } },
        { Direction.Bottom, new List<Direction>() { Direction.Top, Direction.TopRight, Direction.Right, Direction.Left, Direction.TopLeft } },
        { Direction.Left, new List<Direction>() { Direction.Top, Direction.TopRight, Direction.Right, Direction.BottomRight, Direction.Bottom } }
    };

    // Available directions, in which there is a neighbour (he is not null).
    public List<Direction> GetRandomizedAvailableDiggableDirections()
    {
        var res = GetAvailableDiggableDirections();
        res.Shuffle();
        return res;
    }

    public List<MazeUnit> GetDiggableNeighbours()
    {
        return GetAvailableDiggableDirections().ConvertAll(dir => neighbours[dir]);
    }

    public void BecomeNeighbours(Direction direction, MazeUnit other)
    {
        neighbours.Add(direction, other);
        other.neighbours.Add(direction.GetOpposite(), this);
    }

    public bool HasInactiveNeighbours()
    {
        return neighbours.Values.ToList().Any(m => !m.gameObject.activeSelf);
    }

    public bool CanBeDugFrom(Direction direction)
    {
        return gameObject.activeSelf && !isBorder && CheckNeighboursForDigging(direction);
    }

    private List<Direction> GetAvailableDiggableDirections()
    {
        var res = new List<Direction>(diggableDirections);
        res.RemoveAll(dir => !neighbours.ContainsKey(dir));
        return res;
    }

    private bool CheckNeighboursForDigging(Direction direction)
    {
        List<Direction> directionsToCheck = diggableDirectionsCheckGroups[direction];
        directionsToCheck.RemoveAll(dir => !neighbours.ContainsKey(dir));
        List<MazeUnit> neighboursToCheck = directionsToCheck.ConvertAll(dir => neighbours[dir]);
        return neighboursToCheck.TrueForAll(n => n.gameObject.activeSelf);
    }

    public enum Direction
    {
        Top,
        TopRight,
        Right,
        BottomRight,
        Bottom,
        BottomLeft,
        Left,
        TopLeft
    }
}
