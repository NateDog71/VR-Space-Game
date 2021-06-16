using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullDisplay : MonoBehaviour
{
    public GameObject m_HullFull;
    public GameObject m_HullMedium;
    public GameObject m_HullLow;
    public GameObject m_HullEmpty;

    private HullStates m_currentHullState = HullStates.Full;

    public enum HullStates
    {
        Full,
        Medium,
        Low,
        Empty,
    }

    private void Start()
    {
        AssertInspectorInputs();

        SetCurrentStateActive();
    }

    private void AssertInspectorInputs()
    {
        Debug.Assert(m_HullFull != null);
        Debug.Assert(m_HullMedium != null);
        Debug.Assert(m_HullLow != null);
        Debug.Assert(m_HullEmpty != null);
    }

    public void SetHullState(HullStates newHullState)
    {
        Debug.Assert(newHullState != m_currentHullState);

        m_currentHullState = newHullState;

        SetCurrentStateActive();
    }

    private void SetCurrentStateActive()
    {
        m_HullFull.SetActive(false);
        m_HullMedium.SetActive(false);
        m_HullLow.SetActive(false);
        m_HullEmpty.SetActive(false);

        switch(m_currentHullState)
        {
            case HullStates.Full:

                m_HullFull.SetActive(true);
                break;

            case HullStates.Medium:

                m_HullMedium.SetActive(true);
                break;

            case HullStates.Low:

                m_HullLow.SetActive(true);
                break;

            case HullStates.Empty:

                m_HullEmpty.SetActive(true);
                break;

            default:

                Debug.LogError(m_currentHullState);
                break;
        }
    }
}