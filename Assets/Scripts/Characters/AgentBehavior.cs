using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PlatformRunner
{
    public class AgentBehavior : MonoBehaviour
    {
        private NavMeshAgent m_Agent;
        private Animator m_Animator;

        private Transform m_Destination;
        private Vector3 m_StartingPosition;
        private bool m_Racing = false;

        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_Agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            m_Animator.SetFloat("MovementInput", m_Agent.velocity.magnitude);
        }

        public void StartRacing(Transform finishLine)
        {
            m_StartingPosition = transform.position;
            m_Destination = finishLine;
            m_Racing = true;

            m_Agent.SetDestination(m_Destination.position);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Obstacle")
            {
                m_Agent.Warp(m_StartingPosition);

                StartRacing(m_Destination);
            }
        }
    }

}