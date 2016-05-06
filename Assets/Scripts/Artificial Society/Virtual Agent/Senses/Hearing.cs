using UnityEngine;
using System.Collections.Generic;
using Stimuli;

public class Hearing : AbstractSense {
    List<AuralStimulusData> auralStimuli;

    // Use this for initialization
    void Start() {
        auralStimuli = new List<AuralStimulusData>();
    }

    /*
    void OnTriggerEnter(Collider collider) {
        //Debug.Log("Entered: " + collider.name);
        AuralStimulus auralStimulus = collider.gameObject.GetComponent<AuralStimulus>();
        if (auralStimulus != null && !auralStimuli.Contains(auralStimulus.data)) {
            auralStimuli.Add(auralStimulus.data);
        }
    }
    */

    void OnTriggerStay(Collider collider) {
        AuralStimulus auralStimulus = collider.gameObject.GetComponent<AuralStimulus>();
        if (auralStimulus != null && !auralStimuli.Contains(auralStimulus.data)) {
            auralStimuli.Add(auralStimulus.data);
        }
    }

    /*
    void OnTriggerExit(Collider collider) {
        // Debug.Log("Exited: " + collider.name);
        AuralStimulus auralStimulus = collider.gameObject.GetComponent<AuralStimulus>();
        if (auralStimulus != null) {
            if (auralStimuli.Remove(auralStimulus.data)) {
                Debug.Log("Found and removed! " + auralStimulus.name);
            } else {
                // WRYYYYYYYYYY?
                Debug.Log("Didn't remove! " + auralStimulus.name);
            }
        }
    }
    */

    public List<AuralStimulusData> Sense() {
        foreach (AuralStimulusData auralStimulus in auralStimuli) {
            Debug.DrawLine(transform.position, auralStimulus.position, new Color(0, 0, 1, 1), 0.5f);
        }
        List<AuralStimulusData> heard = new List<AuralStimulusData>(auralStimuli);
        auralStimuli.Clear();
        return heard; // Needs to return a copy of the list since it is passed by reference by default.
    }
}