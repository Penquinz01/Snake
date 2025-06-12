using UnityEngine;
using System.Collections.Generic;

public class SnakeHead : MonoBehaviour
{

    InputReader inputReader;
    [SerializeField]private GameObject snakeBodyPrefab;
    public GameObject snakeHeadPrefab { get => snakeBodyPrefab;set => snakeBodyPrefab = value; }
    private SnakeBody snakeBody;
    [SerializeField] private float moveTime;
    private float timer = 0f;
    private Queue<SnakeBody> snakeBodies = new Queue<SnakeBody>();
    private void Awake()
    {
        inputReader = new InputReader();
        snakeBody = GetComponent<SnakeBody>();
        snakeBody.Initialize(this,Vector2.zero);
        snakeBodies.Enqueue(snakeBody);
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timer)
        {
            Move();
            timer = Time.time + moveTime;
        }
        Debug.Log(inputReader.moveInput);
    }
    private void Move()
    {
        if (inputReader.moveInput.x != 0)
        {
            snakeBody.setNextDirection(Vector2.right * inputReader.moveInput.x);
        }
        else if (inputReader.moveInput.y != 0)
        {
            snakeBody.setNextDirection(Vector2.up * inputReader.moveInput.y);
        }
        foreach (var snakes in snakeBodies)
        {
            snakes.Move();
        }
    }
}
