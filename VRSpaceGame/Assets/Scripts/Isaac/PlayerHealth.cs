using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Current health of the enemy
    public float health;

    // Shields health
    public float shields;

    // Timer before shields start recharging
    public float shieldsTimer;

    // Rate the shield recharges at per second
    public float rechargeRate;

    // Image displayed to fade out
    public CourseFader fader;

    // UI reference
    public HullDisplay hullDisplay;

    // Max health for the enemies
    float healthMax;

    // Max health for shields
    float shieldsMax;

    // Maximum timer for shield regen
    float timerMax;

    float alpha = 0;

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

        if (health <= 0)
        {
            fader.TriggerFadeout();
        }
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

        // Update Health UI
        if (health == healthMax)
        {
            hullDisplay.SetHullState(HullDisplay.HullStates.Full);
        }
        else if (health >= healthMax / 2)
        {
            hullDisplay.SetHullState(HullDisplay.HullStates.Medium);
        }
        else if (health < healthMax / 2 && health > 0)
        {
            hullDisplay.SetHullState(HullDisplay.HullStates.Low);
        }
        else
        {
            hullDisplay.SetHullState(HullDisplay.HullStates.Empty);
        }

        // Reset timer for shields to recharge
        shieldsTimer = timerMax;
    }
}
