using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualConsoleHandler : MonoBehaviour
{
    public TMPro.TextMeshPro m_ConsoleText;

    private string m_consoleString;
    public OculusControllerInterface m_OculusControllerInterface;

    public void ToggleConsoleActive()
    {
        bool newActive = !m_ConsoleText.gameObject.activeSelf;

        SetConsoleActive(newActive);
    }

    private void Update()
    {
        m_consoleString = "Local X: " + m_OculusControllerInterface.GetNormalisedRotationX() + "\n";
        m_consoleString += "Local Y: " + m_OculusControllerInterface.GetNormalisedRotationY() + "\n";
        m_consoleString += "Local Z: " + m_OculusControllerInterface.GetNormalisedRotationZ() + "\n";

        UpdateConsoleString();
    }

    public void SetConsoleActive(bool newActive)
    {
        m_ConsoleText.gameObject.SetActive(newActive);
    }

    private void UpdateConsoleString()
    {
        m_ConsoleText.text = m_consoleString;
    }

    public void LogComment(string commentToLog)
    {
        m_consoleString += "\n" + commentToLog;

        UpdateConsoleString();
    }

    public void LogComment(float commentToLog)
    {
        m_consoleString += "\n" + commentToLog.ToString();

        UpdateConsoleString();
    }
}