using UnityEngine;
using UnityEngine.AI;
using System;

namespace PlatformRunner
{
    public class FinishTrigger : MonoBehaviour
    {
        [SerializeField]
        private EmptyEventChannelSO m_OnPlayerFinish;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                m_OnPlayerFinish.RaiseEvent();
            }

            if (other.tag == "Enemy")
            {
                NavMeshAgent agent = other.gameObject.GetComponent<NavMeshAgent>();
                if (agent != null)
                    agent.isStopped = true;
            }
        }
    } 
}
