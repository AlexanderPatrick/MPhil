using UnityEngine;
using System.Collections;

namespace Stimuli {
    public class HapticStimulus : MonoBehaviour {
        public HapticStimulusData data;

        // Update is called once per frame
        void Update() {
            /*
             * Collider[] others = Physics.OverlapSphere(transform.position, 3);
            foreach (Collider other in others) {
                Memory otherMemory = other.GetComponent<Memory>();
                if (otherMemory) {
                    if (!otherMemory.sensoryMemory.hapticMemory.Contains(this)) {
                        otherMemory.sensoryMemory.hapticMemory.Add(this);
                    }
                }
            }
             */
        }
    }
}