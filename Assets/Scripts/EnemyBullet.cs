using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float force;
    [SerializeField] float needRotation;
    [SerializeField] float keepAliveTime = 10;
    [SerializeField] int bulletDamage = 1;

    private GameObject player;
    private Rigidbody2D rb;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");

        Vector3 direction = player.transform.position - this.transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.x, -direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + needRotation);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer > keepAliveTime)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().takeDamage(bulletDamage);
            collision.gameObject.GetComponent<PlayerMovement>().playDamageAnimation();
            Destroy(this.gameObject);
        }else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
