using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] Rigidbody2D bullet_rb;
    [SerializeField] int bullet_speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void AddForce(Vector2 direction) 
    { 
        bullet_rb.AddForce(new Vector2(direction.x*bullet_speed, 0));
    }
    void FixedUpdate()
    {
    }
}
