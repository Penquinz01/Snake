using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    private Vector2 nextDirection;
    private Vector2 currentDirection;
    private SnakeBody nextBody;
    private SnakeHead snakeHead;
    public void Initialize(SnakeHead snakeHead,Vector2 direction)
    {
        currentDirection = direction;
        this.snakeHead = snakeHead;
        nextBody = null;
    }
    public void Move()
    {
        transform.position += (Vector3)currentDirection;

    }
    public void setNextDirection(Vector2 direction) => nextDirection = direction;
    public void newBody()
    {

    }
}
