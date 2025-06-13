using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public void Destroy()
    {
        DestroyImmediate(gameObject);
    }
}
