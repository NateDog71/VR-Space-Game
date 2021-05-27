using UnityEngine;

// Script for shooting a Laser

public class Laser : MonoBehaviour
{
    [Header("Variables")]
    public float damageOverTime = 0;
    public float range = 100;

    [Header("References")]
    public LineRenderer lineRenderer1;
    public LineRenderer lineRenderer2;

    // Ships Firing Points
    public Transform firePoint1;
    public Transform firePoint2;

    //public ParticleSystem impactEffect;
    //public Light impactLight;

    private Enemy targetEnemy;
    private Transform target;
    private AudioSource laserSFX;

    private void Start()
    {
        laserSFX = GetComponent<AudioSource>();
    }

    private void Update()
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

        if (Input.GetMouseButton(0))
        {
            Debug.Log("Pressed");

            Shoot();
        }

        else if (Input.GetMouseButtonUp(0))
        {
            StopShoot();
        }
    }

    private void Shoot()
    {
        //if (targetEnemy.health > 0) targetEnemy.TakeDamage(damageOverTime * Time.deltaTime); // Damage the target

        if (!lineRenderer1.enabled && !lineRenderer2.enabled)
        {
            // Play Laser Sound Effect & Draw Laser line
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
            Debug.Log("Is Target");

            //-----------------------------------------------
            //              Raycast Here
            // -----------------------------------------------

            lineRenderer1.SetPosition(1, target.position);
            lineRenderer2.SetPosition(1, target.position);
        }
        else
        {
            Debug.Log("No Target to Shoot");
            Vector3 laser1Pos = transform.position;
            laser1Pos.x += 100;
            lineRenderer1.SetPosition(1, laser1Pos);

            Vector3 laser2Pos = firePoint2.position;
            laser2Pos.x += 100;
            lineRenderer2.SetPosition(1, laser2Pos);
        }

        Vector3 dir = firePoint1.position - target.position;

        //impactEffect.transform.position = target.position;

        //impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    private void StopShoot()
    {
        lineRenderer1.enabled = false;
        lineRenderer2.enabled = false;
        //impactEffect.Stop();
        //laserSFX.Stop();
        //impactLight.enabled = false;

        Debug.Log("Turn off");
    }
}