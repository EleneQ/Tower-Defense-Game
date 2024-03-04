using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> waypointQueue = new Queue<Waypoint>();
    List<Waypoint> enemyPath = new List<Waypoint>();

    [SerializeField] Waypoint startPoint, endPoint;
    Waypoint currentlySearchingFrom;
    bool shouldSearchForPath = true;

    Vector2Int[] directionsToExplore = {
           Vector2Int.up,
           Vector2Int.right,
           Vector2Int.down,
           Vector2Int.left
    };

    public List<Waypoint> GetEnemyPath()
    {
        if (grid.Count == 0)
        {
            LoadWaypointsIntoGrid();
            CalculatePath();
        }
        return enemyPath;
    }

    private void CalculatePath()
    {
        BreadthFirstSearch();
        RecounstructPathToFollow();
    }

    private void LoadWaypointsIntoGrid()
    {
        Waypoint[] wayPoints = FindObjectsOfType<Waypoint>();

        foreach (Waypoint waypoint in wayPoints)
        {
            var gridPos = waypoint.GetWaypointPosInGrid();
            bool isOverlapping = grid.ContainsKey(gridPos);

            if (!isOverlapping)
            {
                grid.Add(waypoint.GetWaypointPosInGrid(), waypoint);
            }
            else 
            {
                Debug.LogWarning($"Overlapping Block At: {waypoint}");
            }
        }
    }

    private void BreadthFirstSearch()
    {
        waypointQueue.Enqueue(startPoint);

        while(waypointQueue.Count > 0 && shouldSearchForPath)
        {
            currentlySearchingFrom = waypointQueue.Dequeue();
            currentlySearchingFrom.isExplored = true; 

            StopIfPathEndFound();

            ExploreNeighbours(); 
        }
    }

    private void StopIfPathEndFound()
    {
        if(currentlySearchingFrom == endPoint)
        {
            shouldSearchForPath = false;
            return;
        }
    }

    private void ExploreNeighbours()
    {
        foreach (Vector2Int direction in directionsToExplore)
        {
            Vector2Int neighbourCoordinates = currentlySearchingFrom.GetWaypointPosInGrid() + direction;

            if(grid.ContainsKey(neighbourCoordinates))
            {
                QueueNewNeighbours(neighbourCoordinates);
            }
        }
    }

    private void QueueNewNeighbours(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];

        if(neighbour.isExplored || waypointQueue.Contains(neighbour)) { }
        else 
        {
            waypointQueue.Enqueue(neighbour); 
            neighbour.exploredFrom = currentlySearchingFrom;
        }
    }

    private void RecounstructPathToFollow()
    {
        SetAsPath(endPoint);

        Waypoint previous = endPoint.exploredFrom;
 
        while (previous != startPoint)
        {
            SetAsPath(previous);
            previous = previous.exploredFrom;
        }

        SetAsPath(startPoint);

        enemyPath.Reverse();
    }

    void SetAsPath(Waypoint waypoint)
    {
        enemyPath.Add(waypoint);
        waypoint.blockIsPlacable = false;

        OutlinePath(waypoint);
    }

    private void OutlinePath(Waypoint wayPoint)
    {
        wayPoint.GetComponent<Outline>().enabled = true;
    }
}