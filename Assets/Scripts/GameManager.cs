using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [Header("Spawner Initialisation")]
    private float time;
    [SerializeField]private float timeToSpawn = 1f;
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private Vector2Int Range;
    [SerializeField] private GameObject gameStart;
    [SerializeField] private GameObject gameEnd;
    [SerializeField] private GameObject goldenFoodPrefab;
    [SerializeField] private float goldenFoodSpawnTime = 15f;
    private float goldenFoodTime;
    private float timePlayed;
    [SerializeField] private  TMPro.TextMeshProUGUI text;

    public bool IsGameOver { get; private set; } = false;
    public static GameManager Instance { get; private set; }

    public bool started = false;

    private void Awake()
    {
        if (Instance == null){ 
            Instance = this;
        }
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
        time = 0;
        timePlayed = Time.time;
        goldenFoodTime = goldenFoodSpawnTime;
        if (gameStart != null)
        {
            gameStart.SetActive(false);
        }
    }
    private void Start()
    {
        
        Time.timeScale = 1;
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
        if (goldenFoodTime < Time.time)
        {
            x = Random.Range(-Range.x, Range.x);
            y = Random.Range(-Range.y, Range.y);
            spawnPos = new Vector2(x, y);
            goldenFoodTime = Time.time + goldenFoodSpawnTime;
            Instantiate(goldenFoodPrefab, spawnPos, Quaternion.identity);
        }
        time = Time.time + timeToSpawn;
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        IsGameOver = true;
        if (gameEnd == null) return;
        timePlayed = Time.time - timePlayed;
        text.text = $"Time Played: {timePlayed.ToString("F2")}s";
        gameEnd.SetActive(true);
    }
    [Button("Restart")]
    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
