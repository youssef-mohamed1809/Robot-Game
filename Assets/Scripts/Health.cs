using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public int health;
    [SerializeField] int maxHealth = 100;
    private bool isInvincible = false;

    [SerializeField] Image healthBar;
    // Start is called before the first frame update
    void Start(){
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        HealthBarFiller();
        ChangeBarColor();
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

    public void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, 1);
    }

    public void ChangeBarColor()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, (health / maxHealth));
        healthBar.color = healthColor;
    }

}
