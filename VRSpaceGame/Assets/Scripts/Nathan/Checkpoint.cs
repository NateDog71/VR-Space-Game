using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public float radius; // Radius of the checkpoint
    public GameObject arrow; // Arrow pointing which direction

    private PlayerShip.ShipController player;
    public GameObject testPlayer;
    private SphereCollider sphereCollider;

    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>(); // Reference the Sphere Collider
        player = FindObjectOfType<PlayerShip.ShipController>(); // Find the Player

        sphereCollider.radius = radius / 2.43f; // Make the collider on the Checkpoint the same size as the radius chosen
    }

    private void Update()
    {
        if (Vector3.Distance(testPlayer.transform.position, transform.position) <= radius)
        {
            Debug.Log("Entered");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Entered");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Entered");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}