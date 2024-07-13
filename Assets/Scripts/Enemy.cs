using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour {

    private Rigidbody2D rb;
    [Header("Movement")]
    [SerializeField] GameObject pointA;
    [SerializeField] GameObject pointB;
    [SerializeField] float speedx;
    [SerializeField] float speedy;
    [SerializeField] float flipRadius = 0.5f;
    [SerializeField] bool canFly;
    private Transform currentPoint;

    [Header("Follow")]
    [SerializeField] Transform followSource;
    [SerializeField] float followSourceDistance;
    [SerializeField] float followSpeed;
    
    private Vector3 startingPostition;
    private bool outOfPosition;

    //Animation
    private Animator myAnimator;

    // Start is called before the first frame update
    void Start(){
        rb = this.GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
        startingPostition = this.GetComponent<Transform>().position;
        myAnimator = GetComponent<Animator>();
        outOfPosition = false;
    }

    // Update is called once per frame
    private void Update(){

    }
    private void FixedUpdate(){
        Vector2 distance;
        RaycastHit2D hit = Physics2D.Raycast(followSource.position,
            followSource.position - this.transform.position, followSourceDistance);

        myAnimator.SetFloat("Speed", rb.velocity.magnitude);

        if (hit){ /* For the follow algorithm */
            if (hit.collider.gameObject.CompareTag("Player")){
                distance = hit.collider.transform.position - this.transform.position;
                if (!canFly){
                    distance.y = 0;
                } 
                outOfPosition = true;
                rb.AddForce(distance * followSpeed, ForceMode2D.Force);
            }
        } else if (outOfPosition) { /* For the return to position algorithm */

            if (speedx > 0.001f){
                this.transform.position = Vector2.MoveTowards(this.transform.position,
                    startingPostition, speedx * Time.deltaTime);
            } else {
                this.transform.position = Vector2.MoveTowards(this.transform.position,
                    startingPostition, speedy * Time.deltaTime);
                Debug.Log("Y here");
            }

            if (this.transform.position == startingPostition)
            {
                outOfPosition = false;
            }
        } else {

            if (currentPoint == pointB.transform){
                rb.velocity = new Vector2(speedx, speedy);
            } else {
                rb.velocity = new Vector2(-speedx, -speedy);
            }


            if (Vector2.Distance(this.transform.position, currentPoint.position) < flipRadius
                && currentPoint == pointB.transform){
                if (!canFly)
                {
                    flip();
                }
                currentPoint = pointA.transform;
            }

            if (Vector2.Distance(this.transform.position, currentPoint.position) < flipRadius
                && currentPoint == pointA.transform){
                if (!canFly)
                {
                    flip();
                }
                currentPoint = pointB.transform;
            }


        }


    }

    private void flip(){
        Vector3 localScale = this.transform.localScale;
        localScale.x *= -1;
        this.transform.localScale = localScale;
    }

    private void OnDrawGizmos(){
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
        Gizmos.DrawWireSphere(followSource.transform.position, 0.1f);
    }

    public Transform getFollowSource()
    {
        return this.followSource;
    }

    public float getFollowSourceDistance()
    {
        return this.followSourceDistance;
    }
}
