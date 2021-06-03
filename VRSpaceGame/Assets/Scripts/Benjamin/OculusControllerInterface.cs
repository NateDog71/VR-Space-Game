using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusControllerInterface : MonoBehaviour
{
    public bool m_LogInput;

    public VisualConsoleHandler m_VisualConsoleHandler;

    public bool m_IndexTriggerPressed { get; private set; }
    public bool m_IndexTriggerPressedThisFrame { get; private set; }
    public bool m_IndexTriggerReleasedThisFrame { get; private set; }

    public float m_TouchpadVertical { get; private set; }

    private void Start()
    {
        m_VisualConsoleHandler.LogComment("Oculus Controller Interface initialised.");
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
        m_IndexTriggerPressed = OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger);
        m_IndexTriggerPressedThisFrame = OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger);
        m_IndexTriggerReleasedThisFrame = OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger);

        m_TouchpadVertical = Input.GetAxis("Oculus_GearVR_DpadX") * -1;
    }

    private void LogInput()
    {
        if(m_IndexTriggerPressed)
        {
            m_VisualConsoleHandler.LogComment("Index trigger pressed.");
        }

        if(m_IndexTriggerPressedThisFrame)
        {
            m_VisualConsoleHandler.LogComment("Index trigger pressed this frame.");
        }

        if(m_IndexTriggerReleasedThisFrame)
        {
            m_VisualConsoleHandler.LogComment("Index trigger released this frame.");
        }

        if(!Mathf.Approximately(m_TouchpadVertical, 0f))
        {
            m_VisualConsoleHandler.LogComment("Touchpad Vertical: " + m_TouchpadVertical);
        }
    }
}