using UnityEngine;

public class Food : MonoBehaviour,IConsumables
{
    public void Consume(SnakeHead snakeHead)
    {
        snakeHead.Grow();
    }
}
