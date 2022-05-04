using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public class MovingStickObstacle : MonoBehaviour
    {
        private Vector3 m_StartingPosition;
        private Vector3 m_TargetPosition;

        private void Start()
        {
            m_StartingPosition = transform.position;
            m_TargetPosition = transform.position + (-transform.right * 7.0f);

            StartCoroutine(Move(m_TargetPosition, 1.0f));
        }

        IEnumerator Move(Vector3 targetPosition, float duration)
        {
            float timer = 0.0f;

            while (timer < duration)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, timer * 0.1f / duration);
                timer += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPosition;
            if (transform.position == m_StartingPosition)
                StartCoroutine(Move(m_TargetPosition, 1.0f));
            else
                StartCoroutine(Move(m_StartingPosition, 2.0f));
        }
    } 
}
