using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 10f;
    public Vector2 direction;
    int bulletDamage = 5;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().takeDamage(bulletDamage);
            collision.gameObject.GetComponent<PlayerMovement>().playDamageAnimation();
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
