using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerShip
{
    public class ThrustersHandler : MonoBehaviour
    {
        public float m_CurrentThrust { get; private set; }

        [Min(0)]
        [Tooltip("The amount per second by which the current thrust should increase when the user is applying input.")]
        public float m_ThrustIncreaseRate;

        [Min(0)]
        [Tooltip("The amount per second by which the current thrust should decrease when the user is not applying input.")]
        public float m_ThrustDecreaseRate;

        [Min(0)]
        public float m_MaximumForwardsThrust;

        public float m_MaximumBackwardsThrust;

        public ThrustersDial m_ThrustersDial;

        private AudioSource m_thrustersAudioSource;

        public void Initialise()
        {
            AssertInspectorInputs();

            CacheReferences();

            m_thrustersAudioSource.volume = 0f;
        }

        private void AssertInspectorInputs()
        {
            Debug.Assert(m_MaximumForwardsThrust > 0f, m_MaximumForwardsThrust);

            Debug.Assert(m_ThrustIncreaseRate > 0f, m_ThrustIncreaseRate);
            Debug.Assert(m_ThrustDecreaseRate > 0f, m_ThrustDecreaseRate);
        }

        private void CacheReferences()
        {
            m_thrustersAudioSource = GetComponent<AudioSource>();
            Debug.Assert(m_thrustersAudioSource != null);
        }

        public void AddForwardsThrust()
        {
            m_CurrentThrust += m_ThrustIncreaseRate * Time.deltaTime;

            WrapCurrentThrust();
            UpdateCurrentThrustDisplay();

            UpdateThrustersVolume();
        }

        public void AddBackwardsThrust()
        {
            m_CurrentThrust -= m_ThrustDecreaseRate * Time.deltaTime;

            WrapCurrentThrust();
            UpdateCurrentThrustDisplay();

            UpdateThrustersVolume();
        }

        private void WrapCurrentThrust()
        {
            m_CurrentThrust = Mathf.Clamp(m_CurrentThrust, m_MaximumBackwardsThrust, m_MaximumForwardsThrust);
        }

        public void UpdateCurrentThrustDisplay()
        {
            m_ThrustersDial.SetDialValue(GetCurrentThrustPercentage());
        }

        private float GetCurrentThrustPercentage()
        {
            if (m_CurrentThrust >= 0f)
            {
                return m_CurrentThrust / m_MaximumForwardsThrust;
            }
            else
            {
                return m_CurrentThrust / m_MaximumBackwardsThrust * -1;
            }
        }

        private void UpdateThrustersVolume()
        {
            m_thrustersAudioSource.volume = GetCurrentThrustPercentage();
        }
    }
}