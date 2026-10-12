using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.LogError("Kedinin çarptığı obje: " + collision.gameObject.name, collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogWarning("Kedinin girdiği Trigger: " + collision.gameObject.name, collision.gameObject);
    }
}