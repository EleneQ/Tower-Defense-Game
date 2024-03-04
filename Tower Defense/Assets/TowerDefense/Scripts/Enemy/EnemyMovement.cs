using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float stepSpeed = 1f;
    [SerializeField] ParticleSystem pathEndParticle;

    private void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        List<Waypoint> path = pathfinder.GetEnemyPath();

        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
        foreach (Waypoint wayPoint in path)
        {
            var pos = new Vector3(wayPoint.transform.position.x,
                                  transform.position.y, 
                                  wayPoint.transform.position.z);

            transform.position = pos;
            yield return new WaitForSeconds(stepSpeed);
        }

        SelfDestruct();
    }

    private void SelfDestruct()
    {
        ParticleSystem deathParticleObj = Instantiate(pathEndParticle, transform.position, Quaternion.identity);

        deathParticleObj.Play();
        float destroyDelay = deathParticleObj.main.duration;
        Destroy(deathParticleObj.gameObject, destroyDelay);

        Destroy(gameObject);
    }
}