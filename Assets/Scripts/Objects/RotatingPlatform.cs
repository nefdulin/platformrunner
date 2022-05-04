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

        private List<Rigidbody> m_Rigidbodies = new List<Rigidbody>();

        private void Update()
        {
            float rotationDirection = (Clockwise == true) ? -1.0f : 1.0f;
            transform.Rotate(Vector3.forward, rotationDirection * RotationSpeed * Time.deltaTime);

            //foreach (Rigidbody rb in m_Rigidbodies)
            //{
            //    rb.velocity = new Vector3((Vector3.left * Time.deltaTime * RotationSpeed * SlideForce).x, rb.velocity.y, rb.velocity.z);
            //    rb.velocity *= (Clockwise == true) ? -1.0f : 1.0f;
            //}
        }

        private void OnCollisionEnter(Collision collision)
        {
            m_Rigidbodies.Add(collision.gameObject.GetComponent<Rigidbody>());
        }

        private void OnCollisionStay(Collision collision)
        {
            Transform otherTransform = collision.transform;
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        }

        private void OnCollisionExit(Collision collision)
        {
            m_Rigidbodies.Remove(collision.gameObject.GetComponent<Rigidbody>());
        }
    } 
}
