using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace PlatformRunner
{
    public enum GameStatus
    {
        ON_WAIT = 0,
        COUNTDOWN,
        RACING,
        PAINTING,
        FINISHED
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance 
        {
            get
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    instance = new GameObject().AddComponent<GameManager>();
                    instance.gameObject.name = "GameManager(Created)";
                }

                return instance;
            }
        }

        private static GameManager instance;

        [Header("Events")]
        [SerializeField]
        private EmptyEventChannelSO m_OnRaceStart;
        [SerializeField]
        private GameObjectEventChannelSO m_OnPlayerSpawn;
        [SerializeField]
        private IntEventChannelSO m_OnPlayerRankingUpdate;
        [SerializeField]
        private FloatEventChannelSO m_OnCountdownUpdate;
        [SerializeField]
        private EmptyEventChannelSO m_OnPlayerFinishPainting;

        public GameStatus Status { get { return m_Status; } }

        [Header("Spawn")]
        public GameObject PlayerPrefab;
        public GameObject EnemyPrefab;

        public Transform SpawnTransform;
        public float DistanceBetweenEachSpawn;
        public int Row = 2;
        public int Column = 5;

        [Header("Settings")]
        public Transform FinishLine;

        private List<GameObject> m_Enemies = new List<GameObject>();
        private List<Vector3> m_SpawnPositions = new List<Vector3>();

        private GameStatus m_Status;
        private PlayerInputActions m_InputActions;
        private GameObject m_Player;
        private int m_PlayerRanking;
        private bool m_RaceStarted;

        private void Awake()
        {
            instance = this;

            m_InputActions = new PlayerInputActions();
            m_InputActions.Player.Enable();

            m_InputActions.Player.StartRace.performed += StartCountdown;

            m_Status = GameStatus.ON_WAIT;
        }

        private void Start()
        {
            CreateSpawnPositions();
            SpawnPlayers();
        }

        private void Update()
        {
            if (m_Status == GameStatus.RACING)
            {
                int newRanking = CalculatePlayerRanking();

                if (m_PlayerRanking != newRanking)
                {
                    m_PlayerRanking = newRanking;
                    m_OnPlayerRankingUpdate.OnEventRaised(m_PlayerRanking);
                }
            }
        }

        private void CreateSpawnPositions()
        {
            Vector3 spawnPosition = SpawnTransform.position;

            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Column; j++)
                {
                    m_SpawnPositions.Add(new Vector3(spawnPosition.x + (DistanceBetweenEachSpawn * j),
                                                     spawnPosition.y,
                                                     spawnPosition.z + (DistanceBetweenEachSpawn * i)));
                }
            }
        }

        private void SpawnPlayers()
        {
            int playerSpawnIndex = UnityEngine.Random.Range(0, 5);
            m_Player = GameObject.Instantiate(PlayerPrefab, m_SpawnPositions[playerSpawnIndex], Quaternion.identity);
            m_OnPlayerSpawn.RaiseEvent(m_Player);
            
            for (int i = 0; i < Row * Column; i++)
            {
                if (i == playerSpawnIndex)
                    continue;

                m_Enemies.Add(GameObject.Instantiate(EnemyPrefab, m_SpawnPositions[i], Quaternion.identity));
            }
        }

        private int CalculatePlayerRanking()
        {
            int ranking = m_Enemies.Count + 1;

            Vector3 playerPosition = m_Player.transform.position;

            foreach (GameObject enemy in m_Enemies)
            {
                Vector3 enemyPosition = enemy.transform.position;

                if (playerPosition.z > enemyPosition.z)
                    ranking--;
            }

            return ranking;
        }

        private void StartCountdown(InputAction.CallbackContext context)
        {
            StartCoroutine(Countdown());
            m_InputActions.Player.StartRace.performed -= StartCountdown;
        }

        private IEnumerator Countdown()
        {
            float duration = 3.0f;
            m_Status = GameStatus.COUNTDOWN;

            while (duration > 0.0f)
            {
                m_OnCountdownUpdate.RaiseEvent(duration);
                yield return new WaitForSeconds(1.0f);
                duration -= 1.0f;
            }

            StartRace();
        }

        private void StartRace()
        {
            if (!m_RaceStarted)
            {
                m_OnRaceStart.RaiseEvent();

                foreach (GameObject enemy in m_Enemies)
                {
                    enemy.GetComponent<AgentBehavior>().StartRacing(FinishLine);
                }

                m_Status = GameStatus.RACING;
            }
        }

        private void RestartGame(InputAction.CallbackContext context)
        {
            m_InputActions.Player.Restart.performed -= RestartGame;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void OnPlayerFinishRace()
        {
            m_Status = GameStatus.PAINTING;
        }

        public void OnGameOver()
        {
            m_Status = GameStatus.FINISHED;

            m_InputActions.Player.Restart.performed += RestartGame;
        }

        public void PaintPercentageUpdate(float percentage)
        {
            if (percentage == 100)
            {
                m_Status = GameStatus.FINISHED;

                m_OnPlayerFinishPainting.RaiseEvent();

                m_InputActions.Player.Restart.performed += RestartGame;
            }
        }

        private void OnDestroy()
        {
            instance = null;   
        }
    } 
}
