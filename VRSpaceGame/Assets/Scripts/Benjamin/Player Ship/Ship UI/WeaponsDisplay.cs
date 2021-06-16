using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerShip
{
    public class WeaponsDisplay : MonoBehaviour
    {
        public GameObject m_LaserPlane;
        public GameObject m_MissilePlane;

        private void Start()
        {
            SetWeaponDisplay(true);
        }

        public void SetWeaponDisplay(bool laserActive)
        {
            m_LaserPlane.SetActive(laserActive);
            m_MissilePlane.SetActive(!laserActive);
        }
    }
}