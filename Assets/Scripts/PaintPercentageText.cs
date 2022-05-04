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
            m_Text.enabled = false;

            GameManager.Instance.PlayerFinished += OnPlayerFinished;
            GameManager.Instance.PaintPercentageUpdate += OnUpdatePaintPercentage;
        }

        void OnUpdatePaintPercentage(float value)
        {
            Debug.Log(value);
            m_Text.text = "Percentage: " + value;
        }

        private void OnPlayerFinished()
        {
            m_Text.enabled = true;
        }
    }

}