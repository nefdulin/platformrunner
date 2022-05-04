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

            GameManager.Instance.PlayerFinished += OnPlayerFinished;
        }

        private void Update()
        {
            GameManager.Instance.PlayerRankingUpdated += OnPlayerRankingUpdated;
        }

        void OnPlayerRankingUpdated(int newRanking)
        {
            m_Text.text = "Ranking: " + newRanking;
        }
        private void OnPlayerFinished()
        {
            m_Text.enabled = false;
        }
    }
}