using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerShip
{
    public class ShipController : MonoBehaviour
    {
        private Rigidbody m_rigidBody;

        public GameObject m_Camera;
        public WeaponsHandler m_WeaponsHandler;
        public ThrustersHandler m_ThrustersHandler;

        public OculusControllerInterface m_OculusControllerInterface;

        public float m_RotationRate;
        public string m_WeaponSwapButtonName;

        private void Start()
        {
            AssertInspectorInputs();

            CacheReferences();

            InitialiseReferences();
        }

        private void AssertInspectorInputs()
        {
            Debug.Assert(m_Camera != null);

            Debug.Assert(m_WeaponsHandler != null);
            Debug.Assert(m_ThrustersHandler != null);

            Debug.Assert(m_WeaponSwapButtonName != "");
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

            ApplyWeaponsInput();
        }

        private void ApplyThrusterInput()
        {
            if (!Mathf.Approximately(m_OculusControllerInterface.m_TouchpadVertical, 0f) && m_OculusControllerInterface.m_TouchpadVertical > 0f)
            {
                m_ThrustersHandler.AddForwardsThrust();
            }
            else if (!Mathf.Approximately(m_OculusControllerInterface.m_TouchpadVertical, 0f) && m_OculusControllerInterface.m_TouchpadVertical < 0f)
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

        private void ApplyWeaponsInput()
        {
            if(Input.GetMouseButtonDown(0))
            {
                RaycastHit[] hitColliders = Physics.RaycastAll(m_Camera.transform.position, m_Camera.transform.forward, 100.0f);

                foreach(RaycastHit currentHit in hitColliders)
                {
                    if(currentHit.collider.name == m_WeaponSwapButtonName)
                    {
                        m_WeaponsHandler.SwapWeapon();
                    }
                }
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