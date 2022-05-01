using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PlatformRunner
{
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


        [Header("Spawn")]
        public GameObject PlayerPrefab;
        public GameObject EnemyPrefab;

        public Transform SpawnTransform;
        public float DistanceBetweenEachSpawn;
        public int Row = 2;
        public int Column = 5;

        [Header("Settings")]
        public Transform FinishLine;

        public Action<GameObject> PlayerSpawned;
        public Action<int> PlayerRankingUpdated;

        private List<GameObject> enemies = new List<GameObject>();
        private List<Vector3> spawnPositions = new List<Vector3>();

        private GameObject player;
        private int playerRanking;
        private bool raceStarted;

        private void Awake()
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            Vector3 spawnPosition = SpawnTransform.position;

            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Column; j++)
                {
                    spawnPositions.Add(new Vector3(spawnPosition.x + (DistanceBetweenEachSpawn * j),
                                                   spawnPosition.y,
                                                   spawnPosition.z + (DistanceBetweenEachSpawn * i)));
                }
            }

            SpawnPlayers();
        }

        private void Update()
        {
            if (raceStarted)
            {
                int newRanking = CalculatePlayerRanking();

                if (playerRanking != newRanking)
                {
                    playerRanking = newRanking;
                    PlayerRankingUpdated?.Invoke(newRanking);
                }
            }
        }

        private void SpawnPlayers()
        {
            int playerSpawnIndex = UnityEngine.Random.Range(0, 10);
            player = GameObject.Instantiate(PlayerPrefab, spawnPositions[playerSpawnIndex], Quaternion.identity);
            PlayerSpawned?.Invoke(player);
            
            for (int i = 0; i < Row * Column; i++)
            {
                if (i == playerSpawnIndex)
                    continue;

                enemies.Add(GameObject.Instantiate(EnemyPrefab, spawnPositions[i], Quaternion.identity));
            }

            StartRace();
        }

        private int CalculatePlayerRanking()
        {
            int ranking = enemies.Count + 1;

            Vector3 playerPosition = player.transform.position;

            foreach (GameObject enemy in enemies)
            {
                Vector3 enemyPosition = enemy.transform.position;

                if (playerPosition.z > enemyPosition.z)
                    ranking--;
            }

            return ranking;
        }

        private void StartRace()
        {
            raceStarted = true;

            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<AgentBehavior>().StartRacing(FinishLine);
            }
        }
    } 
}
