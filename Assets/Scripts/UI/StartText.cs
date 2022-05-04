using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlatformRunner
{
    public class StartText : MonoBehaviour
    {
        private Text m_Text;

        private void Start()
        {
            m_Text = GetComponent<Text>();
        }

        public void OnRaceStarted()
        {
            m_Text.enabled = false;
        }

        public void OnCountdownUpdate(float value)
        {
            m_Text.text = "Starts in\n" + (int)value;
        }
    }
}