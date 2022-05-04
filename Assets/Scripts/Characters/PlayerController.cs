using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using UnityEngine.AI;

namespace PlatformRunner
{
    public class PlayerController : MonoBehaviour
    {
        public float MovementSpeed = 15.0f;

        [SerializeField]
        private EmptyEventChannelSO m_OnGameOver;

        private NavMeshAgent m_Agent;
        private Animator m_Animator;
        private PlayerInputActions m_InputActions;

        private Vector3 m_MovementDirection = new Vector3(0, 0, 0);
        private float m_MovementInput;

        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_Agent = GetComponent<NavMeshAgent>();

            m_InputActions = new PlayerInputActions();
            m_InputActions.Player.Enable();
        }

        public void Update()
        {
            m_MovementInput = m_InputActions.Player.Movement.ReadValue<float>();

            m_MovementDirection = new Vector3(0.0f, 0.0f, 0.0f);
            if (GameManager.Instance.Status == GameStatus.RACING)
            {
                m_MovementDirection.x = m_MovementInput;
                m_MovementDirection.z = 1;

                m_Agent.Move(m_MovementDirection * m_Agent.speed * Time.deltaTime);
            }

            m_Animator.SetFloat("MovementInput", m_MovementDirection.magnitude);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Obstacle")
            {
                m_Animator.SetBool("IsDead", true);
                m_OnGameOver.RaiseEvent();
            }
        }

        private void OnDisable()
        {
            m_InputActions.Player.Disable();
        }

        private void OnDestroy()
        {
            m_InputActions.Player.Disable();
        }
    } 
}
