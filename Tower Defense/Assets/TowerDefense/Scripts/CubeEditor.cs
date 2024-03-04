using UnityEngine;

[ExecuteInEditMode]
[SelectionBase] 
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{
    int gridSize;
    Waypoint waypoint;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    private void Update()
    {
        gridSize = waypoint.GetGridSize();

        SnapToGrid();
        UpdateTheLable();
    }

    private void SnapToGrid()
    {
        #region Explanation
        //this will make the movement 10 based. it'll make it snap 10 units everytime we move it on the x or z axis.
        //we want cubes to snap 10 units because their width is around 10. essentially, a unit in this world has
        //become 10 normal units worth(or whatever the value of gridsize is).the y corresponds to the z in the other
        //script, so this still works. it's just a mapping from 2d to 3d.
        #endregion
        transform.position = new Vector3(waypoint.GetWaypointPosInGrid().x * gridSize,
                                         0f, 
                                         waypoint.GetWaypointPosInGrid().y * gridSize); 
    }

    private void UpdateTheLable()
    {
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        string labelText = $"{waypoint.GetWaypointPosInGrid().x}, {waypoint.GetWaypointPosInGrid().y}";
        textMesh.text = labelText;      
    }
}