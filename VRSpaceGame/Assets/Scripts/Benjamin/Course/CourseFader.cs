using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseFader : MonoBehaviour
{
    [Min(0f)]
    public float m_FadeSpeed = 1f;

    public string m_MenuSceneName;

    public GameObject m_OculusFader;
    public GameObject m_ComputerFader;

    private Renderer m_oculusRenderer;
    private Renderer m_computerRenderer;

    private bool m_fadingOut = false;
    private Color m_currentFadeColour = Color.black;

    private void Start()
    {
        AssertInspectorInputs();

        CacheReferences();

        m_currentFadeColour.a = 0f;
        ApplyFadeBlackness();
    }

    private void AssertInspectorInputs()
    {
        Debug.Assert(m_MenuSceneName != "");
    }

    private void CacheReferences()
    {
        m_oculusRenderer = m_OculusFader.GetComponent<Renderer>();
        Debug.Assert(m_oculusRenderer != null);

        m_computerRenderer = m_ComputerFader.GetComponent<Renderer>();
        Debug.Assert(m_computerRenderer != null);
    }

    public void TriggerFadeout()
    {
        Debug.Assert(!m_fadingOut);

        m_fadingOut = true;
    }

    private void Update()
    {
        if(m_fadingOut)
        {
            ApplyFadeout();
        }
    }

    private void ApplyFadeout()
    {
        m_currentFadeColour.a += Time.deltaTime * m_FadeSpeed;

        if(m_currentFadeColour.a >= 1f)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(m_MenuSceneName);

            return;
        }

        ApplyFadeBlackness();
    }

    private void ApplyFadeBlackness()
    {
        m_oculusRenderer.material.color = m_currentFadeColour;
        m_computerRenderer.material.color = m_currentFadeColour;
    }
}