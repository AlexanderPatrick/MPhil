using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Stimuli;
using Memories;

namespace Senses {
    /// <summary>
    /// Thermoception is the sense of heat and the absence of heat (cold) by the skin and including internal skin passages, 
    /// or, rather, the heat flux (the rate of heat flow) in these areas.
    /// </summary>
    public class Thermoception : MonoBehaviour {
        public List<ThermalStimulus> thermalStimuli;

        public void Start() {
            thermalStimuli = new List<ThermalStimulus>();
        }

        

        public List<ThermalStimulusData> Sense() {
            List<ThermalStimulusData> thermalStimuliData = new List<ThermalStimulusData>();
            foreach (ThermalStimulus thermalStimulus in thermalStimuli) {
                ThermalStimulusData thermalStimulusDatum = new ThermalStimulusData();
                thermalStimulusDatum.position = thermalStimulus.Position;
                thermalStimulusDatum.temperature = thermalStimulus.Temperature / Vector3.Distance(transform.position, thermalStimulus.Position);
                thermalStimuliData.Add(thermalStimulusDatum);
            }
            return thermalStimuliData;
        }
    }
}