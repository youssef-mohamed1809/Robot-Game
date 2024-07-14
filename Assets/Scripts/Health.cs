using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public int health;
    [SerializeField] int maxHealth = 100;
    private bool isInvincible = false;
    // Start is called before the first frame update
    void Start(){
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addHealth(int added_health)
    {
        if(added_health + health < maxHealth) {
            health += added_health;
        }
        else
        {
            health = maxHealth;
        }
    }

    public void takeDamage(int damage)
    {
        if (!isInvincible)
        {
            health -= damage;
        }
        
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void setIsInvincible(bool isInvincible)
    {
        this.isInvincible = isInvincible;
    }
}
