using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public class CameraBehavior : MonoBehaviour
    {
        public float CameraSpeed = 5.0f;
        public Transform StageTwoTransform;

        private GameObject m_Player;
        private Vector3 m_Offset; 
        private float m_Distance;
        private bool m_RaceFinished = false;

        private void LateUpdate()
        {
            if (m_Player != null)
            {
                if (!m_RaceFinished)
                {
                    Vector3 nextPosition = m_Player.transform.position + m_Offset;
                    transform.position = Vector3.Lerp(transform.position, nextPosition, CameraSpeed * Time.deltaTime);
                }
            }
        }
        public void OnPlayerSpawn(GameObject spawnedPlayer)
        {
            m_Player = spawnedPlayer;
            transform.position = new Vector3(m_Player.transform.position.x, transform.position.y, transform.position.z);
            m_Offset = transform.position - m_Player.transform.position;
        }

        public void OnPlayerFinish()
        {
            m_RaceFinished = true;

            StartCoroutine(MoveTowardsStageTwoPosition(3.0f));
        }

        IEnumerator MoveTowardsStageTwoPosition(float duration)
        {
            float timer = 0.0f;

            while (timer <= duration)
            {
                transform.position = Vector3.Lerp(transform.position, StageTwoTransform.position, timer * 0.1f / duration);
                transform.rotation = Quaternion.Lerp(transform.rotation, StageTwoTransform.rotation, timer * 0.1f / duration);
                timer += Time.deltaTime;
                yield return null;
            }

            transform.position = StageTwoTransform.position;
            transform.rotation = StageTwoTransform.rotation;
        }
    } 
}
