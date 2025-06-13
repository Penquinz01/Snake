using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    private Vector2 nextDirection;
    private Vector2 currentDirection;
    private SnakeBody nextBody;
    private SnakeHead snakeHead;
    private SnakeBody prevBody;
    [SerializeField]private Transform nextSpawn;
    private Queue<Vector3> positionHistory;
    private Rigidbody2D rb;
    private CircleCollider2D col;
    public Transform NextSpawn { get => nextSpawn;private set => nextSpawn = value; }
    protected void Awake()
    {
        positionHistory = new Queue<Vector3>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();

    }
    public void Initialize(SnakeHead snakeHead,SnakeBody prevBody,Vector2 direction)
    {
        currentDirection = direction;
        this.snakeHead = snakeHead;
        this.prevBody = prevBody;
        nextBody = null;
    }
    public void Move()
    {
        if (prevBody!=null)
        {
            positionHistory.Enqueue(prevBody.transform.position);
            Vector3 nextPosition = positionHistory.Dequeue();
            rb.MovePosition(transform.position + (nextPosition-transform.position).normalized);
        }

    }
    public void setNextDirection(Vector2 direction) => nextDirection = direction;
    public void newBody()
    {
        GameObject body = Instantiate(snakeHead.snakeHeadPrefab, transform.position, Quaternion.identity);
        SnakeBody snakeBody = body.GetComponent<SnakeBody>();
        snakeBody.Initialize(snakeHead, this,currentDirection);
        snakeHead.AddToList(snakeBody);

    }
}
