using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public string currentRespawnPointName;
    public Vector2 currentRespawnPointPosition;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        PlayerManager.OnPlayerDeath += ReloadScene;
        PlayerManager.onPlayerReachGoal += LoadNextScene;
    }

    private void OnDisable()
    {
        PlayerManager.OnPlayerDeath -= ReloadScene;
        PlayerManager.onPlayerReachGoal -= LoadNextScene;
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
