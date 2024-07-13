using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Enemy))]
public class EnemyShooting : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletpos;
    [SerializeField] float freq = 2;
    [SerializeField] float shootingDistance = 2;

    private Enemy myEnemy;
    private float timer;
     
    // Start is called before the first frame update
    void Start()
    {
        myEnemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        timer += Time.deltaTime;


        RaycastHit2D hit = Physics2D.Raycast(myEnemy.getFollowSource().position,
            myEnemy.getFollowSource().position - this.transform.position, 
            shootingDistance);

        if (hit)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                if (timer > freq)
                {
                    timer = 0;
                    shoot();
                }
            }
        }

        
    }

    private void shoot() { 
        Instantiate(bullet, bulletpos.position, Quaternion.identity);
    }
}
