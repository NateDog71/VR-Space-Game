using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusControllerInterface : MonoBehaviour
{
    public bool m_LogInput;

    public GameObject m_ControllerAnchor;

    public bool m_IndexTriggerPressed { get; private set; }
    public bool m_IndexTriggerPressedThisFrame { get; private set; }
    public bool m_IndexTriggerReleasedThisFrame { get; private set; }

    public float m_TouchpadVertical { get; private set; }

    public bool m_TouchpadPressed { get; private set; }
    public bool m_TouchpadUpPressedThisFrame { get; private set; }
    public bool m_TouchpadDownPressedThisFrame { get; private set; }

    private bool m_dPadTouchRegistered;

    private void Start()
    {
        VisualConsole.LogComment("Oculus Controller Interface initialised.");
    }

    private void Update()
    {
        CacheUserInput();

        if(m_LogInput)
        {
            LogInput();
        }
    }

    private void CacheUserInput()
    {
        m_TouchpadUpPressedThisFrame = false;
        m_TouchpadDownPressedThisFrame = false;

        m_IndexTriggerPressed = OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger);
        m_IndexTriggerPressedThisFrame = OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger);
        m_IndexTriggerReleasedThisFrame = OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger);

        m_TouchpadVertical = Input.GetAxis("Oculus_GearVR_DpadX") * -1;

        if (!Mathf.Approximately(m_TouchpadVertical, 0f))
        {
            m_TouchpadPressed = true;

            if(!m_dPadTouchRegistered)
            {
                m_dPadTouchRegistered = true;

                if (m_TouchpadVertical > 0f)
                {
                    m_TouchpadUpPressedThisFrame = true;
                }
                else
                {
                    m_TouchpadDownPressedThisFrame = true;
                }
            }
        }
        else if (m_TouchpadPressed)
        {
            m_TouchpadPressed = false;

            m_dPadTouchRegistered = false;
        }
    }

    public float GetNormalisedRotationX()
    {
        float rotationX = m_ControllerAnchor.transform.localEulerAngles.x;

        if (rotationX > 180f)
        {
            rotationX = rotationX - 360f;
        }

        return rotationX;
    }

    public float GetNormalisedRotationY()
    {
        float rotationY = m_ControllerAnchor.transform.localEulerAngles.y;

        if (rotationY > 180f)
        {
            rotationY = rotationY - 360f;
        }

        return rotationY;
    }

    public float GetNormalisedRotationZ()
    {
        float rotationZ = m_ControllerAnchor.transform.localEulerAngles.z;

        if(rotationZ > 180f)
        {
            rotationZ = rotationZ - 360f;
        }

        return rotationZ;
    }

    private void LogInput()
    {
        if(m_IndexTriggerPressed)
        {
            VisualConsole.LogComment("Index trigger pressed.");
        }

        if(m_IndexTriggerPressedThisFrame)
        {
            VisualConsole.LogComment("Index trigger pressed this frame.");
        }

        if(m_IndexTriggerReleasedThisFrame)
        {
            VisualConsole.LogComment("Index trigger released this frame.");
        }

        if(!Mathf.Approximately(m_TouchpadVertical, 0f))
        {
            VisualConsole.LogComment("Touchpad Vertical: " + m_TouchpadVertical);
        }
    }
}