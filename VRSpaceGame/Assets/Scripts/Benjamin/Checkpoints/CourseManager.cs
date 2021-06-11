using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Course
{
    public class CourseManager : MonoBehaviour
    {
        public CheckpointsManager m_CheckpointsManager;

        private void Start()
        {
            InitialiseReferences();
        }

        private void InitialiseReferences()
        {
            m_CheckpointsManager.Initialise(this);
        }

        public void EndCourse()
        {

        }
    }
}