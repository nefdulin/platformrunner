using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlatformRunner
{
    public enum RaceStatus
    {
        ON_WAIT = 0,
        ACTIVE,
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

        public event Action<GameObject> PlayerSpawned;
        public event Action<int> PlayerRankingUpdated;
        public event Action RaceStarted;
        public event Action PlayerFinished;
        public event Action<float> PaintPercentageUpdate;

        public RaceStatus Status { get { return m_Status; } }

        [Header("Spawn")]
        public GameObject PlayerPrefab;
        public GameObject EnemyPrefab;

        public Transform SpawnTransform;
        public float DistanceBetweenEachSpawn;
        public int Row = 2;
        public int Column = 5;

        [Header("Settings")]
        public GameObject WallToPaint;
        public Transform FinishLine;
        public FinishTrigger FinishTrigger;

        private List<GameObject> m_Enemies = new List<GameObject>();
        private List<Vector3> m_SpawnPositions = new List<Vector3>();

        private RaceStatus m_Status;
        private PlayerInputActions m_InputActions;
        private GameObject m_Player;
        private int m_PlayerRanking;
        private bool m_RaceStarted;
        private PaintTracker m_PaintTracker;

        private void Awake()
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            m_InputActions = new PlayerInputActions();
            m_InputActions.Player.Enable();

            m_InputActions.Player.StartRace.performed += StartRace;

            FinishTrigger.PlayerFinished += OnPlayerFinished;

            m_PaintTracker = WallToPaint.GetComponent<PaintTracker>();

            m_PaintTracker.PaintPercentageUpdated += OnPaintPercentageUpdated;

            m_Status = RaceStatus.ON_WAIT;
        }

        private void Start()
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

            SpawnPlayers();
        }

        private void Update()
        {
            if (m_RaceStarted)
            {
                int newRanking = CalculatePlayerRanking();

                if (m_PlayerRanking != newRanking)
                {
                    m_PlayerRanking = newRanking;
                    PlayerRankingUpdated?.Invoke(newRanking);
                }
            }
        }

        private void SpawnPlayers()
        {
            int playerSpawnIndex = UnityEngine.Random.Range(0, 10);
            m_Player = GameObject.Instantiate(PlayerPrefab, m_SpawnPositions[playerSpawnIndex], Quaternion.identity);
            PlayerSpawned?.Invoke(m_Player);
            
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

        private void StartRace(InputAction.CallbackContext context)
        {
            Debug.Log("Enter pressed");

            if (!m_RaceStarted)
            {
                RaceStarted?.Invoke();

                foreach (GameObject enemy in m_Enemies)
                {
                    enemy.GetComponent<AgentBehavior>().StartRacing(FinishLine);
                }

                m_Status = RaceStatus.ACTIVE;
            }
        }

        private void OnPlayerFinished()
        {
            m_Status = RaceStatus.PAINTING;

            PlayerFinished?.Invoke();
        }

        private void OnPaintPercentageUpdated(float value)
        {
            PaintPercentageUpdate?.Invoke(value);
        }
    } 
}
