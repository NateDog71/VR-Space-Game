using UnityEngine;

// Controls the ships shooting and all its weapon types

public class WeaponSystem : MonoBehaviour
{
    [Header("Ship's Variables")]
    // Ships Firing Points
    public Transform laserFirePoint1; // Left Laser Shooting Point
    public Transform laserFirePoint2; // Right Laser Shooting Point

    public Transform missileFirePoint; // Missile Shooting Point

    [Header("Use Laser")]
    public bool useLaser = false; // Is the ship currently using the laser?

    public int damageOverTime = 0; // How much damage the laser does over time
    public int laserRange = 100;   // How far you can shoot a laser
    [Range(0, 10)]
    public float temperature = 1;    // The temperature of the weapon affects how fast the weapons overheat
    [Range(1, 100)]
    public int maxHeat;              // The max temperature the weapon can reach before having to cool down

    public LineRenderer lineRenderer1; // Left Laser
    public LineRenderer lineRenderer2; // Right Laser

    public ParticleSystem impactEffect;
    public ParticleSystem glowEffect;
    public Light impactLight;

    public GameObject laserAudio;   // Laser Audio Reference

    [Header("Use Missile")]
    public bool useMissile = false;  // Is the ship currently using the missile?

    public float missileRange = 100; // How far you can shoot a missile
    [Range(0, 10)]
    public int magSize;             // How much missiles you can shoot before having to reload
    private int actualMagSize;

    public float fireRate = 1f;
    private float fireCountdown = 0f; // Rate of Fire

    public GameObject missilePrefab; // Missile Object it Shoots out
    public GameObject missileAudio;  // Missile Audio Reference

    // Enemy
    private Enemy target;

    // Audio
    private AudioSource laserSFX;
    private AudioSource missileSFX;

    private void Start()
    {
        laserSFX = laserAudio.GetComponent<AudioSource>();
        missileSFX = missileAudio.GetComponent<AudioSource>();

        actualMagSize = magSize;
    }

    private void Update()
    {
        if (lineRenderer1.enabled && lineRenderer2.enabled)
        {
            lineRenderer1.enabled = false;
            lineRenderer2.enabled = false;

            impactEffect.Stop(); // Stop Impact Effect
            glowEffect.Stop();   // Stop Glow Effect
            laserSFX.Stop();     // Stop Sound Effect
            impactLight.enabled = false;
        }

        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.right, laserRange); // Do a raycast

        if (hits.Length == 0) // No Targets Found
        {
            Debug.Log("No Target Found");
            target = null;
        }
        else // RaycastAll Hit Something
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                Debug.Log(hit.transform);

                if (hit.collider.CompareTag("Lock-on")) // If weapons can lock onto target
                {
                    Debug.Log("Locking Onto Target");
                    target = hit.transform.parent.GetComponent<Enemy>(); // Hitting child collider so get parent object
                }
                else if (hit.collider.CompareTag("Enemy")) // If looking directly at enemy
                {
                    Debug.Log("Target Found");
                    target = hit.transform.GetComponent<Enemy>();
                }
            }
        }
        
        if (useLaser)
        {
            if (Input.GetMouseButton(0)) Laser(); // Use Laser
        }
        else if (useMissile)
        {
            if (fireCountdown <= 0)
            {
                if (Input.GetMouseButton(1))
                {
                    Missile(); // Use Missile
                    fireCountdown = 1f / fireRate;
                }
            }
        }
        fireCountdown -= Time.deltaTime;
    }

    // Laser Weapon
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

            target.TakeDamage(damageOverTime); // Damage the target
        }
        else
        {
            Debug.Log("No Target to Shoot");
            Vector3 laser1Pos = transform.position;
            laser1Pos.x += laserRange;
            lineRenderer1.SetPosition(1, laser1Pos);

            Vector3 laser2Pos = laserFirePoint2.position;
            laser2Pos.x += laserRange;
            lineRenderer2.SetPosition(1, laser2Pos);

            // Set Impact Effect Position
            impactEffect.transform.position = laser1Pos;

            // Set Glow Effect Position
            glowEffect.transform.position = laser1Pos;
        }
    }

    // Missile Weapon
    private void Missile()
    {
        if (!target) return; // No Target

        if (lineRenderer1.enabled && lineRenderer2.enabled)
        {
            lineRenderer1.enabled = false;
            lineRenderer2.enabled = false;

            impactEffect.Stop(); // Stop Impact Effect
            glowEffect.Stop();   // Stop Glow Effect
            laserSFX.Stop();     // Stop Sound Effect
            impactLight.enabled = false;
        }

        // No missiles left
        if (magSize <= 0)
        {
            ReloadMissiles();
            return;
        }

        magSize--; // Used a missile

        // Play Missile Sound / Missile Effect & Shoot Missile Prefab
        missileSFX.Play(); // Play Sound Effect

        GameObject missileGo = (GameObject)Instantiate(missilePrefab, missileFirePoint.position, Quaternion.identity);
        Missile missile = missileGo.GetComponent<Missile>();

        // Lock on Target, If there is a target in range shoot it, else shoot in a straight line
        if (missile != null) missile.Seek(target);
    }

    private void ReloadMissiles()
    {
        Debug.Log("Reloading Missiles...");

        magSize = actualMagSize;
    }
}