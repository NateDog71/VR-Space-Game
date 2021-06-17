using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerShip
{
    public class ShipController : MonoBehaviour
    {
        private Rigidbody m_rigidBody;

        public GameObject m_OculusCamera;
        public GameObject m_ComputerCamera;

        public WeaponSystem m_WeaponSystemHandler;
        public ThrustersHandler m_ThrustersHandler;
        public AccelerationShakeHandler m_AccelerationShakeHandler;

        public GameObject m_OVRObject;

        public WeaponsDisplay m_WeaponsDisplay;
        private PlayerHealth m_playerHealthHandler;
        public VisualConsoleOutputHandler m_VisualConsoleHandler;
        public OculusControllerInterface m_OculusControllerInterface;

        [Min(0f)]
        public float m_CollisionDamageMultiplier = 1f;

        [Min(0f)]
        public float m_OculusRollRotationRate = 1f;
        
        [Min(0f)]
        public float m_OculusPitchRotationRate = 1f;

        [Min(0f)]
        public float m_OculusRollRotationSoftener = 1f;
        
        [Min(0f)]
        public float m_OculusPitchRotationSoftener = 1f;

        public float m_ComputerRotationRate;

        public string m_SwapWeaponTag;

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

            Debug.Assert(m_OculusCamera != null);
            Debug.Assert(m_ComputerCamera != null);

            Debug.Assert(m_ThrustersHandler != null);
            Debug.Assert(m_AccelerationShakeHandler != null);

            Debug.Assert(!Mathf.Approximately(m_OculusRollRotationSoftener, 0f));
            Debug.Assert(!Mathf.Approximately(m_OculusPitchRotationSoftener, 0f));

            Debug.Assert(m_SwapWeaponTag != "");
            Debug.Assert(m_OculusControllerInterface != null);
        }

        private void CacheReferences()
        {
            m_rigidBody = GetComponent<Rigidbody>();
            Debug.Assert(m_rigidBody != null); // Assert that the component was found.

            m_playerHealthHandler = GetComponent<PlayerHealth>();
            Debug.Assert(m_playerHealthHandler != null);
        }

        private void InitialiseReferences()
        {
            m_ThrustersHandler.Initialise();

            m_AccelerationShakeHandler.Initialise();
        }

        private void InitialiseMode()
        {
            m_OVRObject.SetActive(GameModeController.m_OculusMode);

            m_ComputerCamera.SetActive(!GameModeController.m_OculusMode);
            m_VisualConsoleHandler.SetConsoleActive(GameModeController.m_OculusMode);
        }

        private void Update()
        {
            ApplyUserInput();
        }

        private void ApplyUserInput()
        {
            ApplyThrusterInput();

            ApplyRotationalInput();

            ApplyTriggerInput();
        }

        private void ApplyThrusterInput()
        {
            if(GameModeController.m_OculusMode)
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
            if(GameModeController.m_OculusMode)
            {
                float rollRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch).y;
                float pitchRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch).x + 0.4f;

                if (Mathf.Abs(rollRotation) < m_OculusRollRotationSoftener)
                {
                    rollRotation = Mathf.Pow(rollRotation * m_OculusRollRotationSoftener, 2f);
                }
                
                if (Mathf.Abs(pitchRotation) < m_OculusPitchRotationSoftener)
                {
                    pitchRotation = Mathf.Pow(pitchRotation * m_OculusPitchRotationSoftener, 2f);
                }

                m_rigidBody.AddTorque(transform.right * m_OculusPitchRotationRate * Time.deltaTime * pitchRotation);
                m_rigidBody.AddTorque(transform.forward * m_OculusRollRotationRate * Time.deltaTime * -rollRotation);
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

        private void ApplyTriggerInput()
        {
            if(!Input.GetMouseButtonDown(0) && !m_OculusControllerInterface.m_IndexTriggerPressedThisFrame) // The player has not applied any selection input.
            {
                return;
            }

            GameObject cameraToUse = GameModeController.m_OculusMode ? m_OculusCamera : m_ComputerCamera;

            RaycastHit[] hitColliders = Physics.RaycastAll(cameraToUse.transform.position, cameraToUse.transform.forward, 100.0f);

            foreach (RaycastHit currentHit in hitColliders)
            {
                if (currentHit.collider.CompareTag(m_SwapWeaponTag))
                {
                    SwapWeapon();

                    VisualConsole.LogComment("Swapped Weapon To: " + (WeaponSystem.useLaser ? "Laser" : "Missile"));

                    return; // Return, as the Swap Weapon button was selected.
                }
            }

            VisualConsole.LogComment("Firing weapon.");
            m_WeaponSystemHandler.FireWeapon(); // Fire the ship's weapon, as no cockpit button was selected.
        }

        private void SwapWeapon()
        {
            WeaponSystem.useLaser = !WeaponSystem.useLaser;
            WeaponSystem.useMissile = !WeaponSystem.useMissile;

            m_WeaponsDisplay.SetWeaponDisplay(WeaponSystem.useLaser);
        }

        private void FixedUpdate()
        {
            UpdateRigidBody();
        }

        private void UpdateRigidBody()
        {
            m_rigidBody.AddForce(transform.forward * m_ThrustersHandler.m_CurrentThrust);
        }

        private void OnCollisionEnter(Collision collision)
        {
            m_playerHealthHandler.TakeDamage(m_rigidBody.velocity.magnitude * m_CollisionDamageMultiplier);
        }
    }
}