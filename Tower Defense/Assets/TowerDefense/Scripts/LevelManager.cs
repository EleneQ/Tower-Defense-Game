using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void LoadGameOver()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);
    }

    public void RestartGame()
    {
        var lastLoadedScene = SceneManager.sceneCount - 1;
        SceneManager.UnloadSceneAsync(lastLoadedScene);

        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
