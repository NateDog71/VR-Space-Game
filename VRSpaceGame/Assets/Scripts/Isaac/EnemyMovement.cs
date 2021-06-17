using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;

    // Object containing the nodes the Enemy moves between
    public GameObject track;

    public Transform parentTransform;
    // First point on track
    Transform pointA;

    // Second point on track
    Transform pointB;

    public float rotateSpeed;

    Transform target;

    Vector3 direction;

    Quaternion lookRotation;

    // Start is called before the first frame update
    void Start()
    {
        pointA = track.transform.GetChild(0);
        pointB = track.transform.GetChild(1);
        parentTransform.position = pointA.position;
        target = pointB;
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(direction);

        direction = target.position - transform.position;

        lookRotation = Quaternion.LookRotation(direction);

        parentTransform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);

        parentTransform.position += transform.forward * speed * Time.deltaTime;

        //  // Determines how far the object should be occilated between points
        //  float Occilation = Mathf.PingPong(Time.time * speed, 1);
        //  // Updates position
        //  transform.position = Vector3.Lerp(pointA.position, pointB.position, Occilation);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Point A")
        {
            target = pointB;
        }
        if (other.gameObject.tag == "Point B")
        {
            Debug.Log("target switched");
            target = pointA;
        }
    }
}
