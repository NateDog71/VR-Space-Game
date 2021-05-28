using UnityEngine;

// Controls the ships shooting and all its weapon types

public class WeaponSystem : MonoBehaviour
{
    [Header("Ship's Variables")]
    // Ships Firing Points
    public Transform firePoint1; // Left Shooting Point
    public Transform firePoint2; // Right Shooting Point

    [Header("Use Laser")]
    public bool useLaser = false; // Is the ship currently using the laser?

    public float damageOverTime = 0; // How much damage the laser does over time
    public float laserRange = 100;   // How far you can shoot a laser
    [Range(0, 10)]
    public float temperature = 1;    // The temperature of the weapon affects how fast the weapons overheat
    [Range(1, 100)]
    public int maxHeat;              // The max temperature the weapon can reach before having to cool down

    public LineRenderer lineRenderer1; // Left Laser
    public LineRenderer lineRenderer2; // Right Laser

    //public ParticleSystem impactEffect;
    //public Light impactLight;

    public GameObject laserAudio;   // Laser Audio Reference

    [Header("Use Missle")]
    public bool useMissle = false;  // Is the ship currently using the missle?

    public float missleDamage = 0;  // How much damage the missle does in 1 shot
    public float missleRange = 100; // How far you can shoot a missle
    [Range(0, 10)]
    public int magSize;             // How much missles you can shoot before having to reload

    public GameObject misslePrefab; // Missle Object it Shoots out

    public GameObject missleAudio;  // Missle Audio Reference

    // Enemy
    private Enemy target;

    // Audio
    private AudioSource laserSFX;
    private AudioSource missleSFX;

    private void Start()
    {
        laserSFX = laserAudio.GetComponent<AudioSource>();
        missleSFX = missleAudio.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (useLaser)
        {
            if (lineRenderer1.enabled && lineRenderer2.enabled)
            {
                lineRenderer1.enabled = false;
                lineRenderer2.enabled = false;
                //impactEffect.Stop();
                //laserSFX.Stop();
                //impactLight.enabled = false;

                Debug.Log("Turn off");
            }
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
            if (Input.GetMouseButton(0)) Laser();
        }
        else if (useMissle)
        {
            if (Input.GetMouseButton(1)) Missle();
        }
    }

    // Laser Weapon
    private void Laser()
    {
        if (!lineRenderer1.enabled && !lineRenderer2.enabled)
        {
            // Play Laser Sound / Laser Effect & Draw Laser line
            Debug.Log("Turn On");
            lineRenderer1.enabled = true;
            lineRenderer2.enabled = true;
            //impactEffect.Play();
            //laserSFX.Play();
            //impactLight.enabled = true;
        }

        // Set the position of the start of the laser (firepoints)
        lineRenderer1.SetPosition(0, firePoint1.position);
        lineRenderer2.SetPosition(0, firePoint2.position);

        // Lock on Target, If there is a target in range shoot it, else shoot in a straight line
        if (target)
        {
            lineRenderer1.SetPosition(1, target.gameObject.transform.position);
            lineRenderer2.SetPosition(1, target.gameObject.transform.position);

            // -----------------------------------------------------------
            //                 Damage target overtime
            // -----------------------------------------------------------
        }
        else
        {
            Debug.Log("No Target to Shoot");
            Vector3 laser1Pos = transform.position;
            laser1Pos.x += laserRange;
            lineRenderer1.SetPosition(1, laser1Pos);

            Vector3 laser2Pos = firePoint2.position;
            laser2Pos.x += laserRange;
            lineRenderer2.SetPosition(1, laser2Pos);
        }

        Vector3 dir = firePoint1.position - target.gameObject.transform.position;

        //impactEffect.transform.position = target.position;

        //impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    // Missle Weapon
    private void Missle()
    {

    }
}