using UnityEngine;
using System;
using System.Collections;
using Senses;

namespace Stimuli {
    public class ThermalStimulus : MonoBehaviour {
        [SerializeField]
        private ThermalStimulusData data;

        // Use this for initialization
        void Start() {
            data.position = Position;
        }

        void Update() {
            data.position = Position;
        }

        public void OnTriggerEnter(Collider collider) {
            Cognition cognition = collider.GetComponent<Cognition>();
            if (cognition != null) {
                Thermoception thermoceptor = cognition.thermoceptors;
                if (thermoceptor != null) {
                    thermoceptor.thermalStimuli.Add(this);
                }
            }
        }

        public void OnTriggerExit(Collider collider) {
            Cognition cognition = collider.GetComponent<Cognition>();
            if (cognition != null) {
                Thermoception thermoceptor = cognition.thermoceptors;
                if (thermoceptor != null) {
                    thermoceptor.thermalStimuli.Add(this);
                }
            }
        }

        public Vector3 Position {
            get {
                return transform.position;
            }
        }

        public float Temperature {
            get {
                return data.temperature;
            }
        }
    }
}