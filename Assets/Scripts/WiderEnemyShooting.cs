using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiderEnemyShooting : MonoBehaviour
{
    [SerializeField] float fieldOfView;
    [SerializeField] LayerMask enemySiteLayers;
    [SerializeField] float shootingRadius = 5f;

    [Header("Bullet Controls")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletpos;
    [SerializeField] float freq = 2;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        Collider2D hit = Physics2D.OverlapCircle(transform.position, shootingRadius, 
            enemySiteLayers);

        if (hit != null && hit.gameObject.CompareTag("Player"))
        {
            if (timer > freq)
            {
                timer = 0;
                shoot();
            }
        }
    }

    private void shoot()
    {
        Instantiate(bullet, bulletpos.position, Quaternion.identity);
    }
}
