using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int enemyHealth = 10;
    [Header("Particles")]
    [SerializeField] ParticleSystem hitParticlesPrefab;
    [SerializeField] ParticleSystem deathParticlesPrefab;

    [Header("Audio")]
    [SerializeField] AudioClip enemyHitSFX;
    [SerializeField] AudioClip enemyDeathSFX;
    AudioSource audioSource;
    Camera cam;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        cam = Camera.main;
    }
    
    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        KillEnemy();
    }

    void ProcessHit()
    {
        enemyHealth--;
        PlayEffects();
    }

    private void PlayEffects()
    {
        hitParticlesPrefab.Play();
        #region Explanation
        //so that we don't get the can not play a disabled audio source exception.
        #endregion
        if (gameObject == null) return; 
        audioSource.PlayOneShot(enemyHitSFX);
    }

    private void KillEnemy()
    {
        if (enemyHealth <= 0)
        {
            ParticleSystem deathParticleObj = Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
            Particles(deathParticleObj);

            #region Explanation
            //all the audio clips are being played in 2d spacial blend, but we can't use 2d when using playclipatpoint, it
            //needs a 3d vector. playing the clip at the pos of the camera's audio listener aka the pos of the camera is the
            //way to simulate the death clip being played with a 2d spacial blend. playing a clip in 3d is saying "play the
            //clip at the position specified, then the player can hear whatever reaches the camera's audio listener + audio
            //effects that make it even quieter(to simulate real-life audio)". 2d basically doesn't take into account the pos
            //of the object a script the audio clip is being played in is attached to.
            #endregion
            AudioSource.PlayClipAtPoint(enemyDeathSFX, cam.transform.position);

            Destroy(gameObject);
        }
    }

    private static void Particles(ParticleSystem deathParticleObj)
    {
        deathParticleObj.Play();
        float destroyDelay = deathParticleObj.main.duration;
        Destroy(deathParticleObj.gameObject, destroyDelay);
    }
}