using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerShip
{
    public class AccelerationShakeHandler : MonoBehaviour
    {
        [Min(0f)]
        public float m_ShakeRate = 1f;

        [Min(0f)]
        public float m_ShakeMaximum = 0.1f;

        private float m_currentShake = 0f;
        private bool m_shakingRight = false;

        public GameObject m_ShipObjectsParent;
        public ThrustersHandler m_ThrustersHandler;

        public void Initialise()
        {
            AssertInspectorInputs();
        }

        private void AssertInspectorInputs()
        {
            Debug.Assert(m_ShipObjectsParent != null);

            Debug.Assert(m_ThrustersHandler != null);
        }

        private void Update()
        {
            ApplyShake();
        }

        private void ApplyShake()
        {
            float shakeThisFrame = Mathf.Abs(m_ThrustersHandler.m_CurrentThrust) * m_ShakeRate * Time.deltaTime;

            if(m_shakingRight)
            {
                shakeThisFrame *= -1;
            }

            m_currentShake += shakeThisFrame;

            if(m_currentShake > m_ShakeMaximum)
            {
                m_shakingRight = true;
            }
            else if(m_currentShake < -m_ShakeMaximum)
            {
                m_shakingRight = false;
            }

            m_ShipObjectsParent.transform.Rotate(m_ShipObjectsParent.transform.forward, shakeThisFrame);
        }
    }
}