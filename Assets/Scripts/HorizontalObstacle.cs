using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public class HorizontalObstacle : MonoBehaviour
    {
        public Transform Target;
        public float MovementSpeed = 10.0f;

        private Vector3 startingPosition;
        private Vector3 currentDestination;
        private float distanceToDestination;

        void Start()
        {
            startingPosition = transform.position;
            currentDestination = Target.position;
        }

        void Update()
        {
            distanceToDestination = Vector3.Distance(transform.position, currentDestination);

            if (Mathf.Approximately(0.0f, distanceToDestination))
            {
                currentDestination = (currentDestination == startingPosition) ? Target.position : startingPosition;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, currentDestination, MovementSpeed * Time.deltaTime);
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            CharacterController controller = collider.GetComponent<CharacterController>();
            if (controller != null)
            {
                Debug.Log("Collision happened");
                Debug.Log("Restart game");
            }
        }
    }

}