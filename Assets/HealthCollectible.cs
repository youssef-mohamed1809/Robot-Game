using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{

    [SerializeField] int healthAdd;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision != null & collision.gameObject.CompareTag("Player"))
        {
            Health player_health = collision.gameObject.GetComponent<Health>();
            player_health.addHealth(healthAdd);
            Destroy(this.gameObject);
        }
    }
}
