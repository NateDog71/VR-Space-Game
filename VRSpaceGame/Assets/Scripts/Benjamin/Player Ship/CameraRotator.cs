using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerShip
{
    public class CameraRotator : MonoBehaviour
    {
        public float m_RotationMultiplier;

        private Vector3 m_mouseDelta;
        private Vector3 m_previousMousePosition;

        private Vector3 m_rotationSetter = Vector3.zero;

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                m_previousMousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(1))
            {
                m_mouseDelta = m_previousMousePosition - Input.mousePosition;

                transform.Rotate(Vector3.up, -m_mouseDelta.x * m_RotationMultiplier);
                transform.Rotate(Vector3.right, m_mouseDelta.y * m_RotationMultiplier);

                m_rotationSetter = transform.eulerAngles;
                m_rotationSetter.z = transform.parent.eulerAngles.z;
                transform.eulerAngles = m_rotationSetter;

                m_previousMousePosition = Input.mousePosition;
            }
        }
    }
}