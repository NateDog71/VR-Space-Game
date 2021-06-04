using UnityEngine;

public class Missile : MonoBehaviour
{
    private Enemy target;

    public int damage;
    public float speed;

    public GameObject impactEffect;

    public void Seek(Enemy _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (!target)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.gameObject.transform.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target.gameObject.transform);
    }

    private void HitTarget()
    {
        target.TakeDamage(damage);

        Destroy(gameObject);
    }
}