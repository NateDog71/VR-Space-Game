using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Course
{
    public class CheckpointsManager : MonoBehaviour
    {
        private CourseManager m_courseManager;

        public TMPro.TextMeshPro m_CheckpointText;

        public GameObject m_CheckpointsParent;
        private Checkpoint[] m_checkpoints;

        private int m_currentCheckpoint = 0;

        public void Initialise(CourseManager inputCourseManager)
        {
            Debug.Assert(inputCourseManager != null);
            m_courseManager = inputCourseManager;

            AssertInspectorInputs();

            InitialiseCheckpoints();
        }

        private void AssertInspectorInputs()
        {
            Debug.Assert(m_CheckpointText != null);
        }

        private void InitialiseCheckpoints()
        {
            Debug.Assert(m_CheckpointsParent.transform.childCount > 0);

            m_checkpoints = new Checkpoint[m_CheckpointsParent.transform.childCount];

            for (int index = 0; index < m_CheckpointsParent.transform.childCount; index++)
            {
                Transform currentChild = m_CheckpointsParent.transform.GetChild(index);

                Checkpoint checkpointComponent = currentChild.GetComponent<Checkpoint>();
                Debug.Assert(checkpointComponent != null);

                m_checkpoints[index] = checkpointComponent;
                m_checkpoints[index].Initialise(this, index);
            }

            m_checkpoints[0].SetCheckpointActive(true);
        }

        public void CheckpointTriggered(int checkpointID)
        {
            VisualConsole.Assert(checkpointID == m_currentCheckpoint, "Triggered checkpoint is not current checkpoint.");

            m_checkpoints[m_currentCheckpoint].SetCheckpointActive(false);

            //Debug.Log("Checkpoint " + m_currentCheckpoint + " hit.");
            m_currentCheckpoint++;

            m_CheckpointText.text = m_currentCheckpoint.ToString();

            if (m_currentCheckpoint >= m_checkpoints.Length)
            {
                m_courseManager.EndCourse();
            }
            else
            {
                m_checkpoints[m_currentCheckpoint].SetCheckpointActive(true);
            }
        }
    }
}