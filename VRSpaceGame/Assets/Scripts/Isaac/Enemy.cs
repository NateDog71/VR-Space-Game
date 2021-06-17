using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    // Explosion particals
    public ParticleSystem explosion;

    // Current health of the enemy
    public float health;

    // Shields health
    public float shields;

    // Timer before shields start recharging
    public float shieldsTimer;

    // Rate the shield recharges at per second
    public float rechargeRate;

    // Sprite for the health bar
    public Image healthBar;

    // Sprite for the shields bar
    public Image shieldsBar;

    // Max health for the enemies
    float healthMax;

    // Max health for shields
    float shieldsMax;

    // Maximum timer for shield regen
    float timerMax;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize max values
        healthMax = health;
        shieldsMax = shields;
        timerMax = shieldsTimer;
    }

    // Update is called once per frame
    void Update()
    {
        // If shields are not at full health, decrease timer
        if (shields < shieldsMax)
        {
            shieldsTimer -= Time.deltaTime;
        }

        // If timer is zero, recharge shields
        if (shieldsTimer < 0)
        {
            shields += Time.deltaTime * rechargeRate;

            if (shields >= shieldsMax)
            {
                // Reset timer to and shields to maximum
                shieldsTimer = timerMax;
                shields = shieldsMax;
            }
        }
        // Disable object after it is dead
        if (health <= 0)
        {
                Instantiate(explosion);
                gameObject.SetActive(false);
        }

        // Get shields as percentage and fill shields bar accordingly
       // shieldsBar.fillAmount = shields / shieldsMax;
    }

    public void TakeDamage(float damage)
    {
        shields -= damage;

        // If shields are depleated deal excess damage to health
        if (shields < 0)
        {
            health += shields;
            shields = 0;
        }

        // Get health as percentage and fill health bar accordingly
        healthBar.fillAmount = health / healthMax;

        

        // Reset timer for shields to recharge
        shieldsTimer = timerMax;
    }
}
