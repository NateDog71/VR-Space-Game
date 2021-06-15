using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerShip
{
    public class WeaponsHandler : MonoBehaviour
    {
        public WeaponTypes m_CurrentWeapon { get; private set; }

        public enum WeaponTypes
        {
            Weapon1,
            Weapon2,
        }

        public TMPro.TextMeshProUGUI m_WeaponTypeText;

        public void SwapWeapon()
        {
            if (m_CurrentWeapon == WeaponTypes.Weapon1)
            {
                m_CurrentWeapon = WeaponTypes.Weapon2;
            }
            else
            {
                m_CurrentWeapon = WeaponTypes.Weapon1;
            }

            UpdateWeaponText();
        }

        private void UpdateWeaponText()
        {
            m_WeaponTypeText.text = ((int)m_CurrentWeapon + 1).ToString();
        }
    }
}