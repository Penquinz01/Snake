using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;

public class SnakeHead : MonoBehaviour
{

    InputReader inputReader;
    [SerializeField] private GameObject snakeBodyPrefab;
    public GameObject snakeHeadPrefab { get => snakeBodyPrefab; private set => snakeBodyPrefab = value; }
    private SnakeBody snakeBody;
    [SerializeField] private float moveTime;
    private float timer = 0f;
    private List<SnakeBody> snakeBodies = new List<SnakeBody>();
    Rigidbody2D rb;
    [SerializeField] private float rayDistance = 0.5f;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask gameOverMask;
    private Direction? currentDirection = null;
    private void Awake()
    {
        inputReader = new InputReader();
        snakeBody = GetComponent<SnakeBody>();
        snakeBody.Initialize(this, null, Vector2.zero);
        snakeBodies.Add(snakeBody);
        rb = GetComponent<Rigidbody2D>();
        inputReader.OnMoveEvent += Move;
    }
    void Start()
    {

    }
    public void AddToList(SnakeBody snake) => snakeBodies.Add(snake);
    // Update is called once per frame
    void Update()
    {
        if (Time.time > timer)
        {
            Move();
            timer = Time.time + moveTime;
        }
        Rotate();
        CheckStatus();
        Debug.Log(inputReader.moveInput);
    }
    private void Move()
    {
        Vector3 dir = Vector3.zero;
        if (inputReader.moveInput.x != 0)
        {
            float sign = Mathf.Sign(inputReader.moveInput.x);
            if ((currentDirection == Direction.Left && sign > 0 || currentDirection == Direction.Right && sign < 0) && snakeBodies.Count > 1)
            {
                Debug.Log("Cannot move in opposite direction");
                sign *= -1;
            }
            dir = Vector3.right * sign;
            currentDirection = sign > 0 ? Direction.Right : Direction.Left;

        }
        else if (inputReader.moveInput.y != 0)
        {
            float sign = Mathf.Sign(inputReader.moveInput.y);
            if ((currentDirection == Direction.Down && sign > 0 || currentDirection == Direction.Up && sign < 0) && snakeBodies.Count > 1)
            {
                Debug.Log("Cannot move in opposite direction");
                sign *= -1;
            }
            dir = Vector3.up * sign;
            currentDirection = sign > 0 ? Direction.Up : Direction.Down;
        }
        rb.MovePosition(transform.position + dir);
        for (int i = 1; i < snakeBodies.Count; i++)
        {
            snakeBodies[i].Move();
        }

    }
    private void CheckStatus()
    {
        var hit = Physics2D.Raycast(transform.position, transform.up, rayDistance, gameOverMask);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("GameOver"))
            {
                Debug.Log("Game Over");
                // Here you can implement game over logic, like restarting the game or showing a game over screen.
                // For now, we will just log it.
            }
            else
            {
                IConsumables consumable = hit.collider.GetComponent<IConsumables>();
                if (consumable != null)
                {
                    consumable.Consume(this);
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
    [Button("Grow")]
    public void Grow()
    {
        SnakeBody snake = snakeBodies[snakeBodies.Count - 1].GetComponent<SnakeBody>();
        snake.newBody();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * rayDistance);
    }
    private void Rotate()
    {
        if (currentDirection == null) return;
        switch (currentDirection)
        {
            case Direction.Left:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case Direction.Right:
                transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case Direction.Up:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case Direction.Down:
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
        }
    }
}
public enum Direction
{
    Left, Right, Up, Down
}
