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
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        time = 0;
        //Spawn();
    }
    private void Update()
    {
        if(Time.time > time)
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
        Time.timeScale = 0;
    }
    [Button("Restart")]
    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        time = 0;
    }
}
