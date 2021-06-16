using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Course
{
    public class CourseManager : MonoBehaviour
    {
        [Min(1)]
        public int m_CourseSeconds;
        private float m_remainingCourseSeconds;

        public CourseFader m_CourseFader;
        public CheckpointsManager m_CheckpointsManager;

        public PlayerShip.CountdownTimer m_CountdownTimer;

        private void Start()
        {
            AssertInspectorInputs();

            InitialiseReferences();

            m_remainingCourseSeconds = m_CourseSeconds;
        }

        private void AssertInspectorInputs()
        {
            Debug.Assert(m_CountdownTimer != null);
        }

        private void InitialiseReferences()
        {
            m_CheckpointsManager.Initialise(this);
        }

        private void Update()
        {
            if (m_remainingCourseSeconds > 0f)
            {
                IncrementCourseCountdown();
            }   
        }

        private void IncrementCourseCountdown()
        {
            m_remainingCourseSeconds -= Time.deltaTime;

            if(m_remainingCourseSeconds > 0f)
            {
                m_CountdownTimer.UpdateCountdownText(m_remainingCourseSeconds);
            }
            else
            {
                m_CountdownTimer.UpdateCountdownText(0f);

                EndCourse();
            }
        }

        public void EndCourse()
        {
            m_CourseFader.TriggerFadeout();
        }
    }
}