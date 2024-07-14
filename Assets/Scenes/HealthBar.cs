using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    [SerializeField] Health myHealth;
    float health, maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        health = myHealth.getHealth();
        maxHealth = myHealth.getMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        health = myHealth.getHealth();
        maxHealth = myHealth.getMaxHealth();
        HealthBarFiller();
        ChangeBarColor();
    }

    public void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health/maxHealth, 1);
    }

    public void ChangeBarColor()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, (health / maxHealth));
        healthBar.color = healthColor;
    }
}
