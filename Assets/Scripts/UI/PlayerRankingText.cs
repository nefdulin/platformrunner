using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace PlatformRunner
{
    public class PlayerRankingText : MonoBehaviour
    {
        private Text m_Text;

        private void Start()
        {
            m_Text = GetComponent<Text>();
        }

        public void OnPlayerRankingUpdate(int newRanking)
        {
            m_Text.text = "Ranking: " + newRanking;
        }
    }
}