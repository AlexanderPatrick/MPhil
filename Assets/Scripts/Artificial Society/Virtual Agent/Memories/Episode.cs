using System;
using System.Collections.Generic;
using Stimuli;
using Emotions;

[Serializable]
public class Episode {
    public List<VisualStimulusData> visualStimuli;
    public List<AuralStimulusData> auralStimuli;
    public List<HapticStimulusData> hapticStimuli;
    public List<GusticStimulusData> gusticStimuli;
    public List<OlfacticStimulusData> olfacticStimuli;
    public PADEmotion emotionalState;
    public float time;

    public PADEmotion previousEmotionalState;
    public PADEmotion emotionDelta;
    public string previousBehaviour;
    public string behaviour;

    public Episode() {
        visualStimuli = new List<VisualStimulusData>();
        auralStimuli = new List<AuralStimulusData>();
        hapticStimuli = new List<HapticStimulusData>();
        gusticStimuli = new List<GusticStimulusData>();
        olfacticStimuli = new List<OlfacticStimulusData>();
        time = 0;
        emotionalState = new PADEmotion();
        previousEmotionalState = new PADEmotion();
        emotionDelta = new PADEmotion();
        previousBehaviour = null;
        behaviour = null;
    }

    public Episode(Episode clone) {
        visualStimuli = clone.visualStimuli;
        auralStimuli = clone.auralStimuli;
        hapticStimuli = clone.hapticStimuli;
        gusticStimuli = clone.gusticStimuli;
        olfacticStimuli = clone.olfacticStimuli;
        time = clone.time;
        emotionalState = clone.emotionalState;
        previousEmotionalState = clone.previousEmotionalState;
        emotionDelta = clone.emotionDelta;
        previousBehaviour = clone.previousBehaviour;
        behaviour = clone.behaviour;
    }
}