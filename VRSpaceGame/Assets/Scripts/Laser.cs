using UnityEngine;

// Script for shooting a Laser

public class Laser : MonoBehaviour
{
    public Transform firePoint1;
    public Transform firePoint2;

    public int damageOverTime = 0;

    public LineRenderer lineRenderer1;
    public LineRenderer lineRenderer2;

    //public ParticleSystem impactEffect;
    //public Light impactLight;

    //private Enemy targetEnemy;
    [SerializeField]
    private Transform target;

    private AudioSource laserSFX;

    private void Start()
    {
        laserSFX = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!target)
        {
            Debug.Log("No Target");

            if (lineRenderer1.enabled && lineRenderer2.enabled)
            {
                lineRenderer1.enabled = false;
                lineRenderer2.enabled = false;
                //impactEffect.Stop();
                //laserSFX.Stop();
                //impactLight.enabled = false;

                Debug.Log("Turn off");
            }

            return;
        }

        if (Input.GetMouseButton(0))
        {
            Debug.Log("Pressed");

            Shoot();
        }

        else if (Input.GetMouseButtonUp(0))
        {

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

        // Start of the Laser is firepoint
        lineRenderer1.SetPosition(0, firePoint1.position);
        lineRenderer2.SetPosition(0, firePoint2.position);

        // Lock on Target

        if (target)
        {
            lineRenderer1.SetPosition(1, target.position);
            lineRenderer2.SetPosition(1, target.position);
        }
        else
        {
            Vector3 laser1Pos = firePoint1.position;
            laser1Pos.y += 100;
            lineRenderer1.SetPosition(1, laser1Pos);

            Vector3 laser2Pos = firePoint2.position;
            laser2Pos.y += 100;
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