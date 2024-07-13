using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 10f;
    public Vector2 direction;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
