using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Stimuli;

namespace Sense {
    public class Touch : MonoBehaviour {
        private List<HapticStimulusData> hapticStimuli;

        void Start() {
            hapticStimuli = new List<HapticStimulusData>();
        }

        void OnCollisionEnter(Collision collision) {
            HapticStimulus hapticStimulus = collision.gameObject.GetComponent<HapticStimulus>();
            if (hapticStimulus != null && !hapticStimuli.Contains(hapticStimulus.data)) {
                hapticStimuli.Add(hapticStimulus.data);
            }
        }

        void OnCollisionExit(Collision collision) {
            HapticStimulus hapticStimulus = collision.gameObject.GetComponent<HapticStimulus>();
            if (hapticStimulus != null) {
                hapticStimuli.Remove(hapticStimulus.data);
            }
        }

        public List<HapticStimulusData> Sense() {
            return new List<HapticStimulusData>(hapticStimuli); // Needs to return a copy of the list since it is passed by reference by default.
        }
    }
}