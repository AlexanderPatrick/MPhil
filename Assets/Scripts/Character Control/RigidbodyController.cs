using UnityEngine;
using System.Collections;

namespace AlexanderPatrick {
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyController : MonoBehaviour {
        public float walkSpeed = 6.0f;
        public float strafeSpeed = 6.0f;
        public float jumpSpeed = 8.0f;

        public float gravity = 20.0F;
        public Vector3 gravityCore;

        private new Rigidbody rigidbody;
        private Vector3 moveDirection = Vector3.zero;
        private bool isGrounded;

        void Start() {
            rigidbody = GetComponent<Rigidbody>();
        }

        void OnCollisionStay(Collision collision) {
            
            foreach(ContactPoint contactPoint in collision.contacts) {
                //Debug.Log(Vector3.Dot(contactPoint.normal, transform.up));
                if (Vector3.Dot(contactPoint.normal, transform.up) > 0.9f) {
                    isGrounded = true;
                    break;
                }
            }
            // Debug.Log(Vector3.Dot(collision.contacts[0].normal);
        }

        void OnCollisionExit(Collision collision) {
            isGrounded = false;
        }

        void FixedUpdate() {
            Vector3 startingLocalVelocity = transform.InverseTransformDirection(rigidbody.velocity);

            Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), (canJump() && Input.GetButton("Jump"))? 1 : 0, Input.GetAxis("Vertical"));

            Vector3 currentLocalVelocity = Vector3.Scale(inputVector, new Vector3(strafeSpeed, jumpSpeed, walkSpeed));
            currentLocalVelocity.y += startingLocalVelocity.y;
            currentLocalVelocity.y = Mathf.Min(currentLocalVelocity.y, jumpSpeed);

            Vector3 worldVelocity = transform.TransformDirection(currentLocalVelocity);
            Vector3 plumbline = (transform.position - gravityCore).normalized;
            worldVelocity -=  plumbline * gravity * Time.fixedDeltaTime;

            rigidbody.velocity = worldVelocity;
            transform.rotation = Quaternion.FromToRotation(transform.up, (transform.position - gravityCore).normalized) * transform.rotation; // Automatically stay upright.
        }

        private bool canJump() {
            if (Physics.Raycast(transform.position, -transform.up, 1.1f)) {
                return true;
            } else {
                return false;
            }
            
        }
    }
}