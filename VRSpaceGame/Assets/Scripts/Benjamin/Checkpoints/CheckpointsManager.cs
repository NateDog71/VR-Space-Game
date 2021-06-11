using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Course
{
    public class CheckpointsManager : MonoBehaviour
    {
        private CourseManager m_courseManager;

        public Checkpoint[] m_Checkpoints;
        private int m_currentCheckpoint = 0;

        public void Initialise(CourseManager inputCourseManager)
        {
            Debug.Assert(inputCourseManager != null);
            m_courseManager = inputCourseManager;

            InitialiseCheckpoints();
        }

        private void InitialiseCheckpoints()
        {
            Debug.Assert(m_Checkpoints.Length > 0);

            for (int index = 0; index < m_Checkpoints.Length; index++)
            {
                m_Checkpoints[index].Initialise(this, index);
            }

            m_Checkpoints[0].SetCheckpointActive(true);
        }

        public void CheckpointTriggered(int checkpointID)
        {
            VisualConsole.Assert(checkpointID == m_currentCheckpoint, "Triggered checkpoint is not current checkpoint.");

            m_Checkpoints[m_currentCheckpoint].SetCheckpointActive(false);

            Debug.Log("Checkpoint " + m_currentCheckpoint + " hit.");
            m_currentCheckpoint++;

            if (m_currentCheckpoint >= m_Checkpoints.Length)
            {
                m_courseManager.EndCourse();
            }
            else
            {
                m_Checkpoints[m_currentCheckpoint].SetCheckpointActive(true);
            }
        }
    }
}