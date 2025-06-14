using UnityEngine;

public class GoldenFood : MonoBehaviour,IConsumables
{
    float time;
    public void Consume(SnakeHead snakeHead)
    {
        snakeHead.DeleteLast();
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
