using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerShip
{
    public class CollisionWarning : MonoBehaviour
    {
        public bool m_WarnAsteroids = true;
        public bool m_WarnCheckpoints = true;
        public bool m_WarnDebris = true;

        [Min(0f)]
        public float m_WarningRange = 1f;

        public GameObject m_PlayerShip;
        public ThrustersHandler m_ThrustersHandler;

        private GameObject m_warningPlane;

        private void Start()
        {
            AssertInspectorInputs();

            CacheReferences();
        }

        private void AssertInspectorInputs()
        {
            Debug.Assert(m_PlayerShip != null);
        }

        private void CacheReferences()
        {
            Debug.Assert(transform.childCount == 1);
            m_warningPlane = transform.GetChild(0).gameObject;
        }

        private void Update()
        {
            m_warningPlane.SetActive(false);

            if(m_ThrustersHandler.m_CurrentThrust > 0f)
            {
                CheckRaycast();
            }
        }

        private void CheckRaycast()
        {
            RaycastHit[] hitColliders = Physics.RaycastAll(m_PlayerShip.transform.position, m_PlayerShip.transform.forward, m_WarningRange);

            foreach(RaycastHit currentHit in hitColliders)
            {
                if(m_WarnAsteroids && currentHit.collider.CompareTag("Asteroid"))
                {
                    m_warningPlane.SetActive(true);
                    return;
                }

                if (m_WarnCheckpoints && currentHit.collider.CompareTag("Checkpoint"))
                {
                    m_warningPlane.SetActive(true);
                    return;
                }

                if (m_WarnDebris && currentHit.collider.CompareTag("Debris"))
                {
                    m_warningPlane.SetActive(true);
                    return;
                }
            }
        }
    }
}