using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [Header("Spawner Initialisation")]
    private float time;
    [SerializeField]private float timeToSpawn = 1f;
    [SerializeField] private GameObject foodPrefab; 
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance = null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    private void Start()
    {
        time = 0;
        Spawn();
    }
    private void Update()
    {
        if(Time.time > time)
        {
            Spawn();
            time = Time.time + timeToSpawn;
        }
    }

    private void Spawn()
    {
        int x = Random.Range(-17, 17);
        int y = Random.Range(-9, 9);
        Vector2 spawnPos = new Vector2(x, y);
        Instantiate(foodPrefab, spawnPos, Quaternion.identity);
    }
}
