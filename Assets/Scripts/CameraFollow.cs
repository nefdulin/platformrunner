using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public class CameraFollow : MonoBehaviour
    {
        public float CameraSpeed = 5.0f;

        private GameObject player;
        private Vector3 offset; 
        private float distance;

        private void Awake()
        {
            GameManager.Instance.PlayerSpawned += OnPlayerSpawned;
        }

        private void OnPlayerSpawned(GameObject spawnedPlayer)
        {
            player = spawnedPlayer;
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            offset = transform.position - player.transform.position;
        }

        private void LateUpdate()
        {
            if (player != null)
            {
                Vector3 nextPosition = player.transform.position + offset;
                transform.position = Vector3.Lerp(transform.position, nextPosition, CameraSpeed * Time.deltaTime);
            }
        }
    } 
}
