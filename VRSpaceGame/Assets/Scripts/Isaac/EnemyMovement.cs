using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;

    // Object containing the nodes the Enemy moves between
    public GameObject track;

    // First point on track
    Transform pointA;

    // Second point on track
    Transform pointB;
    // Start is called before the first frame update
    void Start()
    {
        pointA = track.transform.GetChild(0);
        pointB = track.transform.GetChild(1);
        transform.position = pointA.position; 
    }

    // Update is called once per frame
    void Update()
    {
        // Determines how far the object should be occilated between points
        float Occilation = Mathf.PingPong(Time.time * speed, 1);
        // Updates position
        transform.position = Vector3.Lerp(pointA.position, pointB.position, Occilation);
    }
}
