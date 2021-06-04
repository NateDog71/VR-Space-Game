using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VisualConsole
{
    public static string m_ConsoleText { get; private set; }
    public static bool m_RefreshRequired { get; private set; }

    public static void NotifyConsoleRefreshed()
    {
        m_RefreshRequired = false;
    }

    public static void LogComment(string commentToLog)
    {
        m_ConsoleText += "\n" + commentToLog;

        m_RefreshRequired = true;
    }

    public static void LogComment(float commentToLog)
    {
        m_ConsoleText += "\n" + commentToLog.ToString();

        m_RefreshRequired = true;
    }

    public static void LogError(string errorToLog)
    {
        m_ConsoleText += "\nError: " + errorToLog;

        m_RefreshRequired = true;
    }

    public static void Assert(bool assertionExpression, string assertionFailureMessage)
    {
        if(!assertionExpression)
        {
            m_ConsoleText += "\nAssertion Failed: " + assertionFailureMessage;

            m_RefreshRequired = true;
        }
    }
}