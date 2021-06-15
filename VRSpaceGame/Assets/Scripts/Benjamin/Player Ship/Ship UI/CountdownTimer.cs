using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerShip
{
    public class CountdownTimer : MonoBehaviour
    {
        private TMPro.TextMeshPro m_textComponent;

        private void Start()
        {
            m_textComponent = GetComponent<TMPro.TextMeshPro>();
            Debug.Assert(m_textComponent != null);
        }

        public void UpdateCountdownText(float newCountdownSeconds)
        {
            m_textComponent.text = ((int)newCountdownSeconds).ToString();
        }
    }
}