using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualConsoleOutputHandler : MonoBehaviour
{
    public TMPro.TextMeshPro m_ConsoleText;

    public OculusControllerInterface m_OculusControllerInterface;

    public void ToggleConsoleActive()
    {
        bool newActive = !m_ConsoleText.gameObject.activeSelf;

        SetConsoleActive(newActive);
    }

    private void Update()
    {
        /*
        m_consoleString = "Local X: " + m_OculusControllerInterface.GetNormalisedRotationX() + "\n";
        m_consoleString += "Local Y: " + m_OculusControllerInterface.GetNormalisedRotationY() + "\n";
        m_consoleString += "Local Z: " + m_OculusControllerInterface.GetNormalisedRotationZ() + "\n";
        */

        if(VisualConsole.m_RefreshRequired)
        {
            UpdateConsoleString();
        }
    }

    public void SetConsoleActive(bool newActive)
    {
        m_ConsoleText.gameObject.SetActive(newActive);
    }

    private void UpdateConsoleString()
    {
        m_ConsoleText.text = VisualConsole.m_ConsoleText;

        VisualConsole.NotifyConsoleRefreshed();
    }
}