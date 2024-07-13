using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootingPoint;
    public float shootingInterval = 2f;
    public int bulletsPerShot = 3;
    public float bulletSpread = 10f;
    public float moveSpeed = 2f;
    public Transform player;

    public float patrolRange = 5f;
    private Vector2 patrolStartPoint;
    private bool movingRight = true;

    private float nextShootTime;

    void Start()
    {
        patrolStartPoint = transform.position;
    }

    void Update()
    {
        if (Time.time >= nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + shootingInterval;
        }

        Patrol();
    }

    void Patrol()
    {
        float patrolEndPointX = patrolStartPoint.x + (movingRight ? patrolRange : -patrolRange);
        Vector2 targetPosition = new Vector2(patrolEndPointX, transform.position.y);

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            movingRight = !movingRight;
        }
    }

    void Shoot()
    {
        int bulletsPerShot = Random.Range(1, 6); // Random number of bullets between 1 and 5
        float bulletSpread = Random.Range(10f, 21f); // Random bullet spread between 10 and 20 degrees
        for (int i = 0; i < bulletsPerShot; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
            BossBullet bulletScript = bullet.GetComponent<BossBullet>();

            float angle = (i - bulletsPerShot / 2) * bulletSpread;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.left; // Change Vector2.right to Vector2.left
            bulletScript.direction = direction;
        }
    }
}
