using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;

    public GameObject track;

    Transform pointA;

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
        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(pointA.position, pointB.position, time);
    }
}
