using UnityEngine;
using System.Collections.Generic;
using Stimuli;

public class Taste : AbstractSense {
    public List<GusticStimulus> Sense() {
        return new List<GusticStimulus>();
    }
}
