using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;

public class SnakeHead : MonoBehaviour
{

    InputReader inputReader;
    [SerializeField]private GameObject snakeBodyPrefab;
    public GameObject snakeHeadPrefab { get => snakeBodyPrefab;private set => snakeBodyPrefab = value; }
    private SnakeBody snakeBody;
    [SerializeField] private float moveTime;
    private float timer = 0f;
    private List<SnakeBody> snakeBodies = new List<SnakeBody>();
    Rigidbody2D rb;
    [SerializeField] private float rayDistance = 0.5f;
    [SerializeField]private float speed;
    private void Awake()
    {
        inputReader = new InputReader();
        snakeBody = GetComponent<SnakeBody>();
        snakeBody.Initialize(this,null,Vector2.zero);
        snakeBodies.Add(snakeBody);
        rb = GetComponent<Rigidbody2D>();
        inputReader.OnMoveEvent += Move;
    }
    void Start()
    {
        
    }
    public void AddToList(SnakeBody snake)=>snakeBodies.Add(snake);
    // Update is called once per frame
    void Update()
    {
        if (Time.time > timer)
        {
            Move();
            timer = Time.time + moveTime;
        }
        CheckLose();
        Debug.Log(inputReader.moveInput);
    }
    private void Move()
    {
        Vector3 dir = Vector3.zero;
        if (inputReader.moveInput.x != 0)
        {
            dir = Vector3.right * Mathf.Sign(inputReader.moveInput.x);
        }
        else if (inputReader.moveInput.y != 0)
        {
            dir = Vector3.up * Mathf.Sign(inputReader.moveInput.y);
        }
        rb.MovePosition(transform.position + dir);
        for (int i = 1;i<snakeBodies.Count;i++)
        {
            snakeBodies[i].Move();
        }

    }
    private void CheckLose()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, rayDistance);
        if (hit.collider != null)
        {
            Debug.Log("Hit: " + hit.collider.name);
            // Handle collision with walls or other objects
        }
    }
    [Button("Grow")]
    private void Grow()
    {
        SnakeBody snake = snakeBodies[snakeBodies.Count - 1].GetComponent<SnakeBody>();
        snake.newBody();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * rayDistance);
    }
}
