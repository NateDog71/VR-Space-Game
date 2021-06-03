using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;

    public List<Checkpoint> checkpoints;

    [HideInInspector]
    public int numOfCheckpoints;

    void Start()
    {
        numOfCheckpoints = checkpoints.Count;
    }

    void Update()
    {
        if (numOfCheckpoints == 0)
        {
            Debug.Log("All Checkpoints Reached");
        }
    }
}