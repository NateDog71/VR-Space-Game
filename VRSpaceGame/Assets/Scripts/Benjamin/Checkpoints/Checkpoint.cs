using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Course
{
    public class Checkpoint : MonoBehaviour
    {
        private CheckpointsManager m_checkpointsManager;

        private bool m_isActive;
        private int m_checkpointID = -1;

        public void Initialise(CheckpointsManager inputCheckpointsManager, int inputCheckpointID)
        {
            Debug.Assert(inputCheckpointsManager != null);

            m_checkpointsManager = inputCheckpointsManager;
            m_checkpointID = inputCheckpointID;

            AssertInspectorInputs();

            SetCheckpointActive(false);
        }

        private void AssertInspectorInputs()
        {
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            Debug.Assert(boxCollider != null, name + " needs a Box Collider.");
            Debug.Assert(boxCollider.isTrigger, name + " needs to be a trigger.");
        }

        public void SetCheckpointActive(bool newActive)
        {
            m_isActive = newActive;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(m_isActive)
            {
                if (other.CompareTag("Player"))
                {
                    m_checkpointsManager.CheckpointTriggered(m_checkpointID);
                }
            }
        }
    }
}