using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [Header("Spawner Initialisation")]
    private float time;
    [SerializeField]private float timeToSpawn = 1f;
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private Vector2Int Range;
    [SerializeField] private GameObject gameStart;
    [SerializeField] private GameObject gameEnd;

    public bool IsGameOver { get; private set; } = false;
    public static GameManager Instance { get; private set; }

    public bool started = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        if (gameStart != null)
        {
            gameStart.SetActive(true);
        }
        gameEnd?.SetActive(false);
    }
    public void StartGame()
    {
        started = true;
        if (gameStart != null)
        {
            gameStart.SetActive(false);
        }
    }
    private void Start()
    {
        time = 0;
        Time.timeScale = 1;
        Debug.Log($"Time scale is {Time.timeScale}");
        //Spawn();
    }
    private void Update()
    {
        if(Time.time > time && started)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        int x = Random.Range(-Range.x, Range.x);
        int y = Random.Range(-Range.y, Range.y);
        Vector2 spawnPos = new Vector2(x, y);
        Instantiate(foodPrefab, spawnPos, Quaternion.identity);
        time = Time.time + timeToSpawn;
    }
    public void GameOver()
    {
        Debug.Log("Game over called");
        Time.timeScale = 0;
        IsGameOver = true;
        if (gameEnd == null) return;
        gameEnd.SetActive(true);
    }
    [Button("Restart")]
    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
