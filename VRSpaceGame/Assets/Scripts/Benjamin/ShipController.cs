using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerShip
{
    public class ShipController : MonoBehaviour
    {
        private Rigidbody m_rigidBody;

        public bool m_OculusMode;

        public GameObject m_ComputerCamera;

        public WeaponsHandler m_WeaponsHandler;
        public ThrustersHandler m_ThrustersHandler;

        public GameObject m_OVRObject;

        public VisualConsoleOutputHandler m_VisualConsoleHandler;
        public OculusControllerInterface m_OculusControllerInterface;

        public float m_OculusRotationRate;
        public float m_ComputerRotationRate;

        public string m_WeaponSwapButtonName;

        private void Start()
        {
            AssertInspectorInputs();

            CacheReferences();

            InitialiseReferences();

            InitialiseMode();
        }

        private void AssertInspectorInputs()
        {
            Debug.Assert(m_OVRObject != null);
            Debug.Assert(m_ComputerCamera != null);

            Debug.Assert(m_WeaponsHandler != null);
            Debug.Assert(m_ThrustersHandler != null);

            Debug.Assert(m_WeaponSwapButtonName != "");
            Debug.Assert(m_OculusControllerInterface != null);
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

        private void InitialiseMode()
        {
            m_OVRObject.SetActive(m_OculusMode);

            m_ComputerCamera.SetActive(!m_OculusMode);
            m_VisualConsoleHandler.SetConsoleActive(m_OculusMode);
        }

        private void Update()
        {
            ApplyUserInput();
        }

        private void ApplyUserInput()
        {
            ApplyThrusterInput();

            ApplyRotationalInput();

            //ApplyWeaponsInput();
        }

        private void ApplyThrusterInput()
        {
            if(m_OculusMode)
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
            else
            {
                if(Input.GetKey(KeyCode.W))
                {
                    m_ThrustersHandler.AddForwardsThrust();
                }
                else if(Input.GetKey(KeyCode.S))
                {
                    m_ThrustersHandler.AddBackwardsThrust();
                }
            }
        }

        private void ApplyRotationalInput()
        {
            if(m_OculusMode)
            {
                if (m_OculusControllerInterface.m_IndexTriggerPressed)
                {
                    float relativeRotationX = m_OculusControllerInterface.GetNormalisedRotationX() + 60f;
                    m_rigidBody.AddTorque(transform.right * m_OculusRotationRate * Time.deltaTime * relativeRotationX);

                    float relativeRotationZ = m_OculusControllerInterface.GetNormalisedRotationZ();
                    m_rigidBody.AddTorque(transform.forward * m_OculusRotationRate * Time.deltaTime * relativeRotationZ);
                }
            }
            else
            {
                float scaledRate = m_ComputerRotationRate * Time.deltaTime;

                if (Input.GetKey(KeyCode.Q))
                {
                    m_rigidBody.AddTorque(transform.forward * scaledRate);
                }
                else if (Input.GetKey(KeyCode.E))
                {
                    m_rigidBody.AddTorque(-transform.forward * scaledRate);
                }

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    m_rigidBody.AddTorque(-transform.up * scaledRate);
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    m_rigidBody.AddTorque(transform.up * scaledRate);
                }

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    m_rigidBody.AddTorque(-transform.right * scaledRate);
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    m_rigidBody.AddTorque(transform.right * scaledRate);
                }
            }
        }

        /*
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
        */

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