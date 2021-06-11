using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menus
{
    public class ScoreboardSelectionHandler : MonoBehaviour
    {
        public bool m_OculusMode;

        public GameObject m_OVRCamera;
        public GameObject m_ComputerCamera;

        public GameObject m_ItemSelectionPlane;

        public MenuItem[] m_MenuItems;
        private int m_currentItemIndex = 0;

        public OculusControllerInterface m_OculusControllerInterface;

        private void Start()
        {
            AssertInspectorInputs();

            m_OVRCamera.SetActive(m_OculusMode);
            m_ComputerCamera.SetActive(!m_OculusMode);
        }

        private void AssertInspectorInputs()
        {
            Debug.Assert(m_OculusControllerInterface != null);
        }

        private void Update()
        {
            ApplyUserInput();
        }

        private void ApplyUserInput()
        {
            if(m_OculusMode)
            {

            }
            else
            {
                if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    ModifySelection(false);
                }
                else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    ModifySelection(true);
                }
                else if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
                {
                    LoadSelectedLevel();
                }
            }
        }

        public void ModifySelection(bool moveDown)
        {
            m_currentItemIndex += moveDown ? 1 : -1;
            WrapSelectionIndex();

            UpdateSelectionPosition();
        }

        private void WrapSelectionIndex()
        {
            if(m_currentItemIndex >= m_MenuItems.Length)
            {
                m_currentItemIndex = 0;
            }

            if(m_currentItemIndex < 0)
            {
                m_currentItemIndex = m_MenuItems.Length - 1;
            }
        }

        private void UpdateSelectionPosition()
        {
            m_ItemSelectionPlane.transform.position = m_MenuItems[m_currentItemIndex].m_MenuObject.transform.position;
        }

        private void LoadSelectedLevel()
        {
            string targetScene = m_MenuItems[m_currentItemIndex].m_TargetScene;

            UnityEngine.SceneManagement.SceneManager.LoadScene(targetScene);
        }
    }
}