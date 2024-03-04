using System.Collections.Generic;
using UnityEngine;

public class TurretFactory : MonoBehaviour
{
    [SerializeField] Turret turretPrefab;
    [SerializeField] int turretAmountLimit = 3;
    [SerializeField] Transform turretParent;

    Queue<Turret> turretQueue = new Queue<Turret>();
   
    #region Explanation
    //need waypoint as a parameter because we need to know which waypoint to place the tower on top of.
    #endregion
    public void AddTurret(Waypoint currentWaypoint) 
    {
        int numOfTowers = turretQueue.Count;
        if(numOfTowers < turretAmountLimit)
        {
            SpawnNewTurret(currentWaypoint); 
        }
        else
        {
            MoveExistingTurret(currentWaypoint); 
        }
    }

    private void SpawnNewTurret(Waypoint currentWaypoint)
    {
        Vector3 spawnPos = new Vector3(currentWaypoint.transform.position.x,
                                       turretPrefab.transform.position.y,
                                       currentWaypoint.transform.position.z);
        Turret newTurret = Instantiate(turretPrefab, spawnPos, Quaternion.identity);
        newTurret.transform.parent = turretParent;
  
        currentWaypoint.blockIsPlacable = false;
        newTurret.currentWayPoint = currentWaypoint;

        turretQueue.Enqueue(newTurret);
    }

    //ring buffer aka recycling objects
    private void MoveExistingTurret(Waypoint newBaseWaypoint)
    {
        var oldestTower = turretQueue.Dequeue();

        oldestTower.currentWayPoint.blockIsPlacable = true;
        #region Explanation
        //the waypoint the tower is currently placed on is no longer placable.
        #endregion
        newBaseWaypoint.blockIsPlacable = false; 

        oldestTower.currentWayPoint = newBaseWaypoint;
        oldestTower.transform.position = new Vector3(newBaseWaypoint.transform.position.x,
                                                     oldestTower.transform.position.y,
                                                     newBaseWaypoint.transform.position.z);
        #region Explanation
        //putting the old tower on top of the queue.
        #endregion
        turretQueue.Enqueue(oldestTower);
    }
}