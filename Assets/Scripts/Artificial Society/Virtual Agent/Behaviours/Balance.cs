using UnityEngine;
using System.Collections;

namespace Behaviours {
    /// <summary>
    /// Behaviour to keep a transform upright. Relative to the ground. 
    /// Suction cup shoes Ahoy! Until Gravity gets sorted out.
    /// </summary>
    public class Balance : MonoBehaviour {
        public Transform target;
        private Rigidbody targetRigidbody;

        // Use this for initialization
        void Awake() {
            targetRigidbody = target.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update() {
            // Raycast down and align with the normal of the hit polygon
            RaycastHit hitInfo;
            Physics.Raycast(new Ray(transform.position, -target.up), out hitInfo, 100);
            if (hitInfo.collider != null) {
                target.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
                if (targetRigidbody != null) {
                    targetRigidbody.angularVelocity = Vector3.zero;
                }
            }
        }
    }
}