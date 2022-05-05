using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public class RotatingPlatform : MonoBehaviour
    {
        public float RotationSpeed = 3.0f;
        public float SlideForce = 2.0f;
        public bool Clockwise = false;

        private void Update()
        {
            float rotationDirection = (Clockwise == true) ? -1.0f : 1.0f;
            transform.Rotate(Vector3.forward, rotationDirection * RotationSpeed * Time.deltaTime);
        }
    } 
}
