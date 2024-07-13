using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Enemy))]
public class BigRobotAnim : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator myAnimator;
    private Enemy myEnemy;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myEnemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        myAnimator.SetFloat("Speed", rb.velocity.magnitude);
        RaycastHit2D hit = Physics2D.Raycast(myEnemy.getFollowSource().position,
            myEnemy.getFollowSource().position - this.transform.position,
            myEnemy.getFollowSourceDistance() - 1);

        if (hit)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("Shoot");
            }
        }


    }
}
