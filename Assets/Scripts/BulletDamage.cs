using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class BulletDamage : MonoBehaviour
{
    [SerializeField] int bulletDamage;

    private void OnTriggerEnter2D(Collider2D collision){
        collision.gameObject.GetComponent<Health>().takeDamage(bulletDamage);
        //Destroy(this.gameObject);
    }
}
