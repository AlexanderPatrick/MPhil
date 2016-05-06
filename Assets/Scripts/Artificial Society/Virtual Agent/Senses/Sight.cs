using UnityEngine;
using System.Collections.Generic;
using Stimuli;

namespace Sense {
    public class Sight : MonoBehaviour {
        public float sightRadius = 5;

        public List<VisualStimulusData> Sense() {
            List<VisualStimulusData> visualStimuli = new List<VisualStimulusData>();
            Collider[] seenObjects = Physics.OverlapSphere(transform.position, sightRadius);
            foreach (Collider seenObject in seenObjects) {
                VisualStimulus visualStimulus = seenObject.GetComponent<VisualStimulus>();
                if (visualStimulus != null) {
                    visualStimuli.Add(visualStimulus.data);
                    Debug.DrawLine(transform.position, visualStimulus.data.position, Color.yellow, 0.5f);
                }
            }
            return visualStimuli;
        }
    }
}
