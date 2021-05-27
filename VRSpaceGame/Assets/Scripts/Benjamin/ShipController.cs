using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerShip
{
    public class ShipController : MonoBehaviour
    {
        private Rigidbody m_rigidBody;

        public ThrustersHandler m_ThrustersHandler;

        public float m_RotationRate;

        private void Start()
        {
            CacheReferences();

            InitialiseReferences();
        }

        private void CacheReferences()
        {
            m_rigidBody = GetComponent<Rigidbody>();
            Debug.Assert(m_rigidBody != null); // Assert that the component was found.
        }

        private void InitialiseReferences()
        {
            m_ThrustersHandler.Initialise();
        }

        private void Update()
        {
            ApplyUserInput();
        }

        private void ApplyUserInput()
        {
            ApplyThrusterInput();

            ApplyRotationalInput();
        }

        private void ApplyThrusterInput()
        {
            if (Input.GetKey(KeyCode.W))
            {
                m_ThrustersHandler.AddForwardsThrust();
            }
            else if (Input.GetKey(KeyCode.S))
            {
                m_ThrustersHandler.AddBackwardsThrust();
            }
        }

        private void ApplyRotationalInput()
        {
            float rate = m_RotationRate * Time.deltaTime;

            if(Input.GetKey(KeyCode.Q))
            {
                m_rigidBody.AddTorque(transform.forward * rate);
            }
            else if(Input.GetKey(KeyCode.E))
            {
                m_rigidBody.AddTorque(-transform.forward * rate);
            }

            if(Input.GetKey(KeyCode.LeftArrow))
            {
                m_rigidBody.AddTorque(-transform.up * rate);
            }
            else if(Input.GetKey(KeyCode.RightArrow))
            {
                m_rigidBody.AddTorque(transform.up * rate);
            }
            
            if(Input.GetKey(KeyCode.UpArrow))
            {
                m_rigidBody.AddTorque(-transform.right * rate);
            }
            else if(Input.GetKey(KeyCode.DownArrow))
            {
                m_rigidBody.AddTorque(transform.right * rate);
            }
        }

        private void FixedUpdate()
        {
            UpdateRigidBody();
        }

        private void UpdateRigidBody()
        {
            m_rigidBody.AddForce(transform.forward * m_ThrustersHandler.m_CurrentThrust);
        }
    }
}