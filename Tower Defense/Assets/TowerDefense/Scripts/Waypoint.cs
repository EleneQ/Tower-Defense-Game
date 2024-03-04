using UnityEngine;
using UnityEngine.EventSystems;

#region Explanation
//will hold data about waypoints / cubes, like where they are in the world, have they already been explored or not and so on.
#endregion
public class Waypoint : MonoBehaviour 
{
    [SerializeField] Outline outline;

    [HideInInspector]
    public bool isExplored = false;

    public Waypoint exploredFrom { get; set; }

    [HideInInspector]
    public bool blockIsPlacable = true;

    #region Explanation
    //this isn't just public because the whole set up of the world depends on it and we don't want to be able to modify it
    //from another script. this should have the same value for every waypoint/cube, that's why it's a const.
    #endregion
    const int gridSize = 10;
    public int GetGridSize() { return gridSize; }

    public Vector2Int GetWaypointPosInGrid() 
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize));
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && blockIsPlacable &&
            !EventSystem.current.IsPointerOverGameObject())
        {
            FindObjectOfType<TurretFactory>().AddTurret(this);     
        }
    }
}