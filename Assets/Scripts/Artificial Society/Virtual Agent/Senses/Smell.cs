using UnityEngine;
using System.Collections.Generic;
using Stimuli;

public class Smell : AbstractSense {
    List<OlfacticStimulus> olfacticStimuli;

	// Use this for initialization
	void Start () {
        olfacticStimuli = new List<OlfacticStimulus>();
	}


    void OnTriggerEnter(Collider collider) {
        Stimulus stimulusComponent = collider.gameObject.GetComponent<Stimulus>();
        OlfacticStimulus olfacticStimulus = null;
        if (stimulusComponent != null) {
            olfacticStimulus = stimulusComponent.olfacticStimulus;
        }
        
        if (olfacticStimulus != null && !olfacticStimuli.Contains(olfacticStimulus)) {
            olfacticStimuli.Add(olfacticStimulus);
            // OnAddOlfacticStimulus(olfacticStimulus)
        }
    }

    void OnTriggerExit(Collider collider) {
        Stimulus stimulusComponent = collider.gameObject.GetComponent<Stimulus>();
        OlfacticStimulus olfacticStimulus = null;
        if (stimulusComponent != null) {
            olfacticStimulus = stimulusComponent.olfacticStimulus;
        }

        if (olfacticStimulus != null) {
            olfacticStimuli.Remove(olfacticStimulus);
        }
    }

    public void Sense() {
        foreach (OlfacticStimulus olfacticStimulus in olfacticStimuli) {
            Debug.DrawLine(transform.position, olfacticStimulus.Position, new Color(0.5f,0,1,1), 0.5f);
        }
    }
}
