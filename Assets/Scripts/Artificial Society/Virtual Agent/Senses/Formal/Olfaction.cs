using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Stimuli;
using Memories;

namespace Senses {
    /// <summary>
    /// Olfaction is the other "chemical" sense. There are hundreds of olfactory receptors (388 according to one source), 
    /// each binding to a particular molecular feature. 
    /// </summary>
    public class Olfaction : MonoBehaviour {
        public List<OlfacticStimulus> olfacticStimuli;

        public void Start() {
            olfacticStimuli = new List<OlfacticStimulus>();
        }

        public List<OlfacticStimulusData> Sense() {
            List<OlfacticStimulusData> olfacticStimuliData = new List<OlfacticStimulusData>();
            foreach (OlfacticStimulus olfacticStimulus in olfacticStimuli) {
                OlfacticStimulusData olfacticStimulusDatum = new OlfacticStimulusData();
                olfacticStimulusDatum.position = olfacticStimulus.Position;
                olfacticStimulusDatum.scent = olfacticStimulus.Scent / Vector3.Distance(transform.position, olfacticStimulus.Position);
                olfacticStimuliData.Add(olfacticStimulusDatum);
            }
            return olfacticStimuliData;
        }
    }
}