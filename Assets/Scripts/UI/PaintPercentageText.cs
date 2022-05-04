using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlatformRunner
{
    public class PaintPercentageText : MonoBehaviour
    {
        private Text m_Text;
        private void Start()
        {
            m_Text = GetComponent<Text>();
        }

        public void OnUpdatePaintPercentage(float value)
        {
            m_Text.text = "Percentage: " + value;
        }
    }

}