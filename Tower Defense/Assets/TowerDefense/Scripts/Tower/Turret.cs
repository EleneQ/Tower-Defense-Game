using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform headTransform;  
    [SerializeField] float attackRange = 20f;
    [SerializeField] ParticleSystem projectileParticle;

    public Waypoint currentWayPoint; 

    [SerializeField] Transform targetEnemy;

    private void Update()
    {
        SetTargetEnemy();
        if (targetEnemy == null) return; 

        headTransform.LookAt(targetEnemy);
        FireAtEnemy();
    }

    #region Explanation
    //making it so that each tower finds the enmy closest to it and focuses on that enemy. if there are no enemies, the
    //towers do nothing.
    #endregion
    private void SetTargetEnemy()
    {
        var enemies = FindObjectsOfType<EnemyMovement>();
        if(enemies.Length == 0) { return; }

        #region Explanation
        //so that the towers have something to focust on right off the bat.
        #endregion
        Transform closestEnemy = enemies[0].transform; 
        #region Explanation
        //checking the distance to other enemies to find if there is a closer enemy for this tower to focus on. if there
        //isn't, it'll keep focusing on the fist enemy in the array.
        #endregion
        foreach (EnemyMovement testEnemy in enemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        }

        targetEnemy = closestEnemy;
    }

    private Transform GetClosest(Transform startEnemy, Transform testEnemy)
    {
        #region Explanation
        //finding the distance between this tower and the first enemy in the array and the distance bwtween all the
        //enemies in the array to compare them below and find which one's closer to this tower to set that one as the
        //targetenemy.
        #endregion
        var distanceToA = Vector3.Distance(transform.position, startEnemy.position);
        var distanceToB = Vector3.Distance(transform.position, testEnemy.position);

        if(distanceToA < distanceToB)
        {
            return startEnemy;
        }

        return testEnemy;
    }

    private void FireAtEnemy()
    {
        float distanceToEnemy = (targetEnemy.position - transform.position).magnitude;
        if (distanceToEnemy <= attackRange)
        {
            Shoot(true);
        }
        else Shoot(false);
    }

    private void Shoot(bool isActive)
    {
        var emissionModule = projectileParticle.emission;
        emissionModule.enabled = isActive;
    }
}