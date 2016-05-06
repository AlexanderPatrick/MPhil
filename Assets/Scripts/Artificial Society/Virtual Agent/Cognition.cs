using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Memories;
using Emotions;
using Senses;
using Stimuli;

public class Cognition : MonoBehaviour {
    public float updatesPerSecond = 1;

    public Memory memory;
    public Emotion emotion;

    // Sensors
    public Vision eyes;
    public Audition ears;
    public Olfaction nose;
    public Chronoception clock;
    public Thermoception thermoceptors;
    public Equilibrioception equilibrioceptors;

    private float cognitionTimer = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > cognitionTimer) {
            cognitionTimer = Time.time + 1 / updatesPerSecond;
            float time = clock.Sense();
            float balance = equilibrioceptors.Sense();
            // Debug.Log("{Tick:" + time + " Balance:" + balance + "}");

            List<VisualStimulusData> visualStimuli = eyes.Sense();
            memory.sensoryMemory.iconicMemory = visualStimuli;
            // List<VisualStimulusData> newStimuli = visualStimuli.Except<VisualStimulusData>(memory.sensoryMemory.iconicMemory).ToList<VisualStimulusData>();
            
            List<ThermalStimulusData> thermalStimuli = thermoceptors.Sense();
            memory.sensoryMemory.thermalMemory = thermalStimuli;

            List<OlfacticStimulusData> olfacticStimuli = nose.Sense();
            memory.sensoryMemory.olfacticMemory = olfacticStimuli;

            PADEmotion currentEmotion = new PADEmotion();

            // Temperature Adjustment
            if (memory.sensoryMemory.thermalMemory[0].temperature > 0.5 && memory.sensoryMemory.thermalMemory[0].temperature < 0.6) { // All of these ugly hard-coded values
                currentEmotion.valence += 0.1f;
            }

            // Smell Adjustment
            if (memory.sensoryMemory.olfacticMemory.Count > 0) {
                if (memory.sensoryMemory.olfacticMemory.Max<OlfacticStimulusData>((OlfacticStimulusData data) => { return data.scent; }) > 10) { // That have no scientific backing
                    currentEmotion.valence += 0.2f;
                }
            }

            // Sight Adjustment
            if (memory.sensoryMemory.iconicMemory != null) {

            }

            emotion.pad = currentEmotion;

            Debug.Log("Emotion: (" + emotion.pad.valence + "," + emotion.pad.arousal + "," + emotion.pad.dominance + ")");


        }
	}
}
