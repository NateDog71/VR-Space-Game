using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerShip
{
    public class ThrustersDial : MonoBehaviour
    {
        public GameObject m_DialPointer;

        private Vector3 m_localAnglesSetter = Vector3.zero;

        public void SetDialValue(float newThrusterPercentage)
        {
            m_localAnglesSetter.y = -90f * newThrusterPercentage;

            m_DialPointer.transform.localEulerAngles = m_localAnglesSetter;
        }
    }
}