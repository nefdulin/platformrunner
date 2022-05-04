using UnityEngine;
using System;

namespace PlatformRunner
{
    public class FinishTrigger : MonoBehaviour
    {
        public event Action PlayerFinished;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                PlayerFinished?.Invoke();
            }
        }
    } 
}
