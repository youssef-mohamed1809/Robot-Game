
using UnityEngine;


public class BulletDamage : MonoBehaviour
{
    [SerializeField] int bulletDamage;

    private void OnCollisionEnter2D(Collision2D collision){
        
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        { 
            collision.gameObject.GetComponent<Health>().takeDamage(bulletDamage);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        
    }
}
