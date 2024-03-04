using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 10;
    [SerializeField] HealthBar healthBar;
    bool isAlive = true;

    private void Start()
    {
        healthBar.SetMaxHealth(health);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && health > 0)
        {
            health--;
            healthBar.SetHealth(health);
        }
        
        if(other.CompareTag("Enemy") && health <= 0 && isAlive)
        {
            isAlive = false;

            LevelManager.instance.LoadGameOver();
            Time.timeScale = 0f;
        }
    }
}