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

        private CharacterController characterController;
        private NavMeshAgent agent;
        private Rigidbody rigidbody;
        private Animator animator; 
        private PlayerInputActions inputActions;

        private Vector3 movementDirection = new Vector3(0, 0, 0);
        private float movementInput;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();

            inputActions = new PlayerInputActions();
            inputActions.Player.Enable();
        }

        public void Update()
        {
            movementInput = inputActions.Player.Movement.ReadValue<float>();

            // Better than calling new every update
            movementDirection.x = (movementInput * MovementSpeed);
            movementDirection.z = (1 * MovementSpeed);


            agent.Move(movementDirection * Time.deltaTime);
            //transform.Translate(movementDirection * Time.deltaTime);

            animator.SetFloat("MovementInput", movementDirection.magnitude);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Fired from player");
        }

        private void OnDisable()
        {
            inputActions.Player.Disable();
        }

        private void OnDestroy()
        {
            inputActions.Player.Disable();
        }
    } 
}
