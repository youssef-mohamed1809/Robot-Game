using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class BulletDamage : MonoBehaviour
{
    [SerializeField] int bulletDamage;

    private void OnCollisionEnter2D(Collision2D collision){
        
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        { 
            collision.gameObject.GetComponent<Health>().takeDamage(bulletDamage);
        }else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        
    }
}
