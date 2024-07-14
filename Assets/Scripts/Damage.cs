
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] int damage = 2;
    private Health health;

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Player"))
        {

            if(health == null)
            {
                health = collision.gameObject.GetComponent<Health>();
            }
            collision.gameObject.GetComponent<PlayerMovement>().playDamageAnimation();
            health.takeDamage(damage);
        }
    }
}
