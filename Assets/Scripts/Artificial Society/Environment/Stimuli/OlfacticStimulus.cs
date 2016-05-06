using UnityEngine;
using System.Collections;
using Senses;

namespace Stimuli {
    [RequireComponent(typeof(SphereCollider))]
    public class OlfacticStimulus : MonoBehaviour {
        [SerializeField]
        private OlfacticStimulusData data;
        private SphereCollider sphereCollider;

        // Use this for initialization
        void Start() {
            sphereCollider = GetComponent<SphereCollider>();
            sphereCollider.isTrigger = true;
            data.radius = sphereCollider.radius;
            data.position = Position;
        }

        void Update() {
            data.position = Position;
        }

        public void OnTriggerEnter(Collider collider) {
            Cognition cognition = collider.GetComponent<Cognition>();
            if (cognition != null) {
                Olfaction nose = cognition.nose;
                if (nose != null) {
                    nose.olfacticStimuli.Add(this);
                }
            }
        }

        public void OnTriggerExit(Collider collider) {
            Cognition cognition = collider.GetComponent<Cognition>();
            if (cognition != null) {
                Olfaction nose = cognition.nose;
                if (nose != null) {
                    nose.olfacticStimuli.Remove(this);
                }
            }
        }

        public Vector3 Position {
            get {
                return transform.position;
            }
        }

        public float Scent {
            get {
                return data.scent;
            }
        }
    }
}