using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public class RotatingPlatform : MonoBehaviour
    {
        public float RotationSpeed = 3.0f;
        public float SlideForce = 2.0f;

        private void Update()
        {
            Vector3 rotation = new Vector3(0, 0, 1.0f);
            transform.Rotate(Vector3.forward, RotationSpeed * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Collision happened");
        }

        private void OnCollisionStay(Collision collision)
        {
            Transform otherTransform = collision.transform;
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.velocity = new Vector3((Vector3.left * Time.deltaTime * RotationSpeed * SlideForce).x, rb.velocity.y, rb.velocity.z);
        }
    } 
}
