using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f, 7f)]
    [SerializeField] float spawnDelay = 2f;
    #region Explanation
    //this is of type enemymovement because we want to be able to drag in only an enemy and not just any gameobject or transform.
    #endregion
    [SerializeField] EnemyMovement enemyPrefab;

    AudioSource audioSource;
    [SerializeField] AudioClip spawnSFX;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while(true)
        {
            audioSource.PlayOneShot(spawnSFX);

            var enemyObj = Instantiate(enemyPrefab, enemyPrefab.transform.position, enemyPrefab.transform.rotation);
            enemyObj.gameObject.transform.parent = transform;

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}