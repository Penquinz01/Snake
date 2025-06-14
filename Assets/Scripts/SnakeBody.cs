using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{

    private SnakeHead snakeHead;
    private SnakeBody prevBody;
    [SerializeField]private Transform nextSpawn;
    private Queue<Vector3> positionHistory;
    private Rigidbody2D rb;
    public Transform NextSpawn { get => nextSpawn;private set => nextSpawn = value; }
    protected void Awake()
    {
        positionHistory = new Queue<Vector3>();
        rb = GetComponent<Rigidbody2D>();

    }
    public void Initialize(SnakeHead snakeHead,SnakeBody prevBody)
    {
        this.snakeHead = snakeHead;
        this.prevBody = prevBody;
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
    public void newBody()
    {
        GameObject body = Instantiate(snakeHead.snakeHeadPrefab, transform.position, Quaternion.identity);
        SnakeBody snakeBody = body.GetComponent<SnakeBody>();
        snakeBody.Initialize(snakeHead, this);
        snakeHead.AddToList(snakeBody);

    }
}
