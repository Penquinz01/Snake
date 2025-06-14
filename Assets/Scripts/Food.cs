using UnityEngine;

public class Food : MonoBehaviour,IConsumables
{
    float time;
    public void Consume(SnakeHead snakeHead)
    {
        snakeHead.Grow();
    }
    private void Start()
    {
        time = 0;
    }
    private void Update()
    {
        if (time > 8f)
        {
            Destroy(gameObject);
        }
    }
}
