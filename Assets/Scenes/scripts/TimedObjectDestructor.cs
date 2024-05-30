using UnityEngine;

public class TimedObjectDestructor : MonoBehaviour
{
    public float timeOut = 1.0f;

    void Awake()
    {
        Invoke("DestroyNow", timeOut);
    }

    void DestroyNow()
    {
        Destroy(gameObject);
    }
}