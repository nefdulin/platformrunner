using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PlatformRunner
{
    public class AgentBehavior : MonoBehaviour
    {

        private NavMeshAgent agent;
        private Animator animator;

        private Transform destination;
        private Vector3 staringPosition;
        private bool racing = false;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            //if (racing)
            //{
            //    agent.SetDestination(destination.position);
            //}
            
            animator.SetFloat("MovementInput", agent.velocity.magnitude);
        }

        public void StartRacing(Transform finishLine)
        {
            staringPosition = transform.position;
            destination = finishLine;
            racing = true;

            agent.SetDestination(destination.position);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Obstacle")
            {
                transform.position = staringPosition;
            }
        }

    }

}