using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody2D))]
public class BigRobot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletpos;
    [SerializeField] float freq = 2;
    [SerializeField] float shootingDistance = 2;
    [SerializeField] float BlockingTime = 2;


    private Enemy myEnemy;
    private Health myHealth;
    private float speedx;

    [Header("Sheild")]
    [SerializeField] float blockRepeatation = 10;
    [SerializeField] float blockDuration = 3;
    private float timer;
    private float speedTimer = 10;
    private float blockingTimer;
    private float blockingTimer2;
    private bool notBlocking;
    

    //Animation
    private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myEnemy = this.GetComponent<Enemy>();
        myHealth = this.GetComponent<Health>();
        myAnimator = this.GetComponent<Animator>();
        notBlocking = true;
        blockingTimer = blockRepeatation;
        blockingTimer2 = blockRepeatation + blockDuration;
        speedx = myEnemy.getSpeedx();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        blockingTimer -= Time.deltaTime;
        blockingTimer2 -= Time.deltaTime;
        speedTimer -= Time.deltaTime;

        RaycastHit2D hit = Physics2D.Raycast(myEnemy.getFollowSource().position,
            myEnemy.getFollowSource().position - this.transform.position,
            shootingDistance);
        
        if (hit)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                this.speedTimer = 10;
                myEnemy.setSpeedx(0);
                if (timer > freq && notBlocking)
                {
                    timer = 0;
                    shoot();
                    myAnimator.SetTrigger("Shooting");
                }

                if (blockingTimer <= 0)
                {
                    blockingTimer = blockRepeatation;
                    myHealth.setIsInvincible(true);
                    notBlocking = false;
                    myAnimator.SetBool("NotBlocking", notBlocking);
                    myAnimator.SetTrigger("Blocking");
                }

                if (blockingTimer2 <= 0)
                {
                    blockingTimer2 = blockRepeatation + blockDuration;
                    notBlocking = true;
                    myHealth.setIsInvincible(false);
                    myAnimator.SetBool("NotBlocking", notBlocking);
                }
            }
        }

        if(speedTimer <= 0)
        {
            speedTimer = 10;
            myEnemy.setSpeedx(speedx);
        }

    }

    private void shoot()
    {
        Instantiate(bullet, bulletpos.position, Quaternion.identity);
    }

   
}
