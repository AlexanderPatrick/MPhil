using UnityEngine;
using System.Collections;

namespace Behaviours {
    public class Motion : AbstractBehaviour {
        private new Rigidbody rigidbody;
        private Vector3 movementVector;
        private Vector3 torqueVector;

        // Use this for initialization
        void Start() {
            rigidbody = GetComponent<Rigidbody>();
            movementVector = Vector3.zero;
        }

        void FixedUpdate() {
            rigidbody.AddForce(movementVector, ForceMode.Force);
            rigidbody.AddTorque(torqueVector, ForceMode.Force);
        }

        public void StartMovingForward() {
            if (rigidbody) {
                Debug.Log("Moving Forward");
                movementVector = transform.forward * 7;
            } else {
                Debug.Log("Can't Move Forward, no rigidbody.");
            }
        }

        public void StopMoving() {
            if (rigidbody) {
                Debug.Log("Stopping Moving");
                movementVector = Vector3.zero;
                rigidbody.velocity = Vector3.zero;
            } else {
                Debug.Log("Can't Stop Moving, no rigidbody.");
            }
        }

        public void StartMovingBackward() {
            if (rigidbody) {
                Debug.Log("Moving Backward");
                movementVector = -transform.forward * 7;
            } else {
                Debug.Log("Can't Move Backward, no rigidbody.");
            }
        }

        public void StartTurningLeft() {
            if (rigidbody) {
                Debug.Log("Turning Left");
                torqueVector = Vector3.down * 2;
            } else {
                Debug.Log("Can't Turn Left, no rigidbody.");
            }
        }

        public void StopTurning() {
            if (rigidbody) {
                Debug.Log("Stopping Turning");
                torqueVector = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
            } else {
                Debug.Log("Can't Stop Turning, no rigidbody.");
            }
        }

        public void StartTurningRight() {
            if (rigidbody) {
                Debug.Log("Turning Right");
                torqueVector = Vector3.up * 2;
            } else {
                Debug.Log("Can't Turn Right, no rigidbody.");
            }
        }

        public void Jump() {
            if (rigidbody) {
                Debug.Log("Jumping");
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, 5, rigidbody.velocity.z);
            } else {
                Debug.Log("Can't Jump, no rigidbody.");
            }
        }
    }
}