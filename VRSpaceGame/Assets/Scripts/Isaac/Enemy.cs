using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Total health of the enemy
    public int health;

    // Shields health
    public float shields;

    // Timer before shields start recharging
    public float shieldsTimer;

    // Rate the shield recharges at per second
    public float rechargeRate;

    // Max health for shields
    float shieldsMax;

    // Maximum timer
    float timerMax;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize shields and timer maximums
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
    }

    public void TakeDamage(int damage)
    {
        shields -= damage;

        // If shields are depleated deal excess damage to health
        if (shields < 0)
        {
            health += (int)shields;
            shields = 0;
        }
        
        // Reset timer for shields to recharge
        shieldsTimer = timerMax;
    }
}
