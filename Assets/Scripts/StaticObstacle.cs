using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public class StaticObstacle : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            CharacterController controller = other.GetComponent<CharacterController>();

            if (controller != null)
            {
                Debug.Log("Trigger gameover event");
            }
        }
    } 
}
