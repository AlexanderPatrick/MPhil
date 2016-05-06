using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Stimuli;
using Memories;

namespace Senses {
    /// <summary>
    /// Vision is the capability of the eyes to focus and detect images of visible light on photoreceptors in the retina of each eye
    /// that generates electrical nerve impulses for varying colors, hues, and brightness.
    /// </summary>
    public class Vision : MonoBehaviour {
        public float visionRadius = 5;
        public List<VisualStimulusData> Sense() {
            List<VisualStimulusData> visualStimuli = new List<VisualStimulusData>();
            Collider[] seenObjects = Physics.OverlapSphere(transform.position, visionRadius);
            foreach (Collider seenObject in seenObjects) {
                VisualStimulus visualStimulus = seenObject.GetComponent<VisualStimulus>();
                if (visualStimulus != null) {
                    // The old way which just gets what the object reports
                    //visualStimuli.Add(visualStimulus.data);

                    // The new way where the Vision script pulls the data itself.
                    VisualStimulusData data = new VisualStimulusData();
                    data.position = visualStimulus.Position;
                    data.colour = visualStimulus.Colour;
                    data.shape = visualStimulus.Shape;
                    visualStimuli.Add(data);
                }
            }
            return visualStimuli;
        }
    }
}