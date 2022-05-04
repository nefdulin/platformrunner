using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public class HorizontalObstacle : MonoBehaviour
    {
        public Transform Target;
        public float MovementSpeed = 10.0f;

        private Vector3 m_StartingPosition;
        private Vector3 m_CurrentDestination;
        private float m_DistanceToDestination;
        void Start()
        {
            m_StartingPosition = transform.position;
            m_CurrentDestination = Target.position;
        }

        void Update()
        {
            m_DistanceToDestination = Vector3.Distance(transform.position, m_CurrentDestination);

            if (Mathf.Approximately(0.0f, m_DistanceToDestination))
            {
                m_CurrentDestination = (m_CurrentDestination == m_StartingPosition) ? Target.position : m_StartingPosition;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, m_CurrentDestination, MovementSpeed * Time.deltaTime);
            }
        }
    }

}