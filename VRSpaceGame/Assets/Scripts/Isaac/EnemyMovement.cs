using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    ///////// THIS SHOULD BE ADDED THE THE HULL OF THE SHIP, THE WEAPONS CANNOT BE A CHILD OF THE OBJECT THIS SCRIPT IS ATTACHED TO

    // Rate at which the player moves
    public float speed;

    // Object containing the nodes the Enemy moves between
    public GameObject track;

    // Highest parented object in the hierarchy
    public Transform parentTransform;

    public EnemyWeapons weaponsSystem;

    // Speed the player rotates at
    public float rotateSpeed;

    // Amount speed is reduced by when rotating
    public float rotationDampener;

    // Current target
    Transform target;

    // direction of the target
    Vector3 direction;

    // Rotation required for enemy to face the target
    Quaternion lookRotation;

    int trackIndex = 1;

    float timer = 1;

    // Start is called before the first frame update
    void Start()
    {
        parentTransform.position = track.transform.GetChild(0).position;

        // Set start position to point a and target position to second point
        target = track.transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponsSystem.inRange)
        {
            target = weaponsSystem.target.transform;
        }
        else
        {
            target = track.transform.GetChild(trackIndex);
        }
        timer -= Time.deltaTime;

        // Get direction from ship hull to target
        direction = target.position - transform.position;

        // Get the required rotation to be facing target
        lookRotation = Quaternion.LookRotation(direction);

        // Apply appropriate rotation
        parentTransform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);

        Quaternion quaternion = new Quaternion(1, 1, 1, 1);

        // Update position
        if (Quaternion.Angle(parentTransform.rotation, lookRotation) < 10)
            parentTransform.position += transform.forward * speed * Time.deltaTime;
        else
            parentTransform.position += transform.forward * speed * Time.deltaTime * rotationDampener;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Point" && timer < 0)
        {
            trackIndex++;
            if (trackIndex == track.transform.childCount)
            {
                trackIndex = 0;
            }
            
            target = track.transform.GetChild(trackIndex);
            Debug.Log(track.transform.childCount);
            Debug.Log(target.gameObject.name);
            timer = 1;
        }
    }
}
