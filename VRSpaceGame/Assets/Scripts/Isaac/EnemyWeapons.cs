using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapons : MonoBehaviour
{
    public LineRenderer lineRenderer1;

    public LineRenderer lineRenderer2;

    public ParticleSystem impactEffect;

    public ParticleSystem glowEffect;
    
    public Light impactLight;

    public Transform laserFirePoint1;

    public Transform laserFirePoint2;

    public GameObject target;

    public float laserDamage;

    public float lockOnTimer;

    public float rangeMax;

    private float timerMax;

    private AudioSource laserSFX;

    [HideInInspector]
    // Is the player in range
    public bool inRange;

    private PlayerHealth player;
    // Start is called before the first frame update
    void Start()
    {
        timerMax = lockOnTimer;
        player = target.GetComponent<PlayerHealth>();
        laserSFX = transform.GetChild(1).GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, target.transform.position);
        if (dist < rangeMax)
        {
            lockOnTimer -= Time.deltaTime;
            inRange = true;
        }
        else
        {
            inRange = false;
            lockOnTimer = timerMax;
        }
        if (lockOnTimer < 0)
        {
            Laser();
            player.TakeDamage(laserDamage);
        }


    }

    private void Laser()
    {
        if (!lineRenderer1.enabled && !lineRenderer2.enabled)
        {
            // Play Laser Sound / Laser Effect & Draw Laser line
            lineRenderer1.enabled = true;
            lineRenderer2.enabled = true;

            impactEffect.Play(); // Play Impact Effect
            glowEffect.Play();   // Play Glow Effect
            laserSFX.Play();     // Play Sound Effect
            impactLight.enabled = true;
        }

        // Set the position of the start of the laser (firepoints)
        lineRenderer1.SetPosition(0, laserFirePoint1.position);
        lineRenderer2.SetPosition(0, laserFirePoint2.position);

        // Lock on Target, If there is a target in range shoot it, else shoot in a straight line
        if (target)
        {
            lineRenderer1.SetPosition(1, target.gameObject.transform.position);
            lineRenderer2.SetPosition(1, target.gameObject.transform.position);

            Vector3 dir = target.gameObject.transform.position;

            // Set Impact Effect Position
            impactEffect.transform.position = target.gameObject.transform.position;
            impactEffect.transform.rotation = Quaternion.LookRotation(dir);

            // Set Glow Effect Position
            glowEffect.transform.position = target.gameObject.transform.position;
            glowEffect.transform.rotation = Quaternion.LookRotation(dir);

            // Damage the target
        }
    }
}
