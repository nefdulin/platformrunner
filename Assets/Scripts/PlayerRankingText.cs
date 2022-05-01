using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace PlatformRunner
{
    public class PlayerRankingText : MonoBehaviour
    {
        private Text text;

        private void Start()
        {
            text = GetComponent<Text>();
        }

        private void Update()
        {
            GameManager.Instance.PlayerRankingUpdated += OnPlayerRankingUpdated;
        }

        void OnPlayerRankingUpdated(int newRanking)
        {
            text.text = "Ranking: " + newRanking;
        }
    }
}