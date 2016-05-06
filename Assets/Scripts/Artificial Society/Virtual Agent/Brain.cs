using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Sense;

using Touch = Sense.Touch; // Thanks UnityEngine Namespace
using Motion = Behaviours.Motion; // Thanks UnityEngine Namespace

using Senses;
using Emotions;
using Memories;
using Behaviours;

using Stimuli;

public class Brain : MonoBehaviour {
    public float updatesPerSecond = 1;

    private Emotion emotion;
    private Memory memory;
    // Sensors
    private Sight eyes;
    private Hearing ears;
    private Smell nose;
    private Taste tongue;
    private Touch skin;
    private Hunger stomach;
    private Chronoception circadianRhythm;
    // Actuators
    private Voice mouth;
    private Motion body;

    #region Alertness Properties
    [HideInInspector] 
    [SerializeField] 
    private float globalAlertness = 1; // Range 0 (No Alertness) - 1 (Perfect Alertness) Global Alertness can be a modifier for anxiety, awake, sleeping, coma, death
    private float timeToSense = 0;
    [ExposeProperty]
    public float GlobalAlertness {
        get { return globalAlertness; }
        set { 
            value = Mathf.Clamp01(value);
            if (value < float.Epsilon) {
                timeToSense = Mathf.Infinity;
            } else {
                timeToSense = timeToSense - 1 /(updatesPerSecond*globalAlertness) + 1 /(updatesPerSecond*value); // subtract old adjustment and add new adjustment
            }
            globalAlertness = value;
        }
    }

    // Sense specific alertness and timers
    [HideInInspector]
    [SerializeField]
    private float sightAlertness = 1;
    private float timeToSee = 0;
    [ExposeProperty]
    public float SightAlertness {
        get { return sightAlertness; }
        set {
            value = Mathf.Clamp01(value);
            if (value * GlobalAlertness < float.Epsilon) {
                timeToSee = Mathf.Infinity;
            } else {
                timeToSee = timeToSee - 1 / (updatesPerSecond * sightAlertness * GlobalAlertness) + 1 / (updatesPerSecond * value * GlobalAlertness); // subtract old adjustment and add new adjustment
            }
            sightAlertness = value;
        }
    }

    [HideInInspector]
    [SerializeField]
    private float hearingAlertness = 1;
    private float timeToHear = 0;
    [ExposeProperty]
    public float HearingAlertness {
        get { return hearingAlertness; }
        set {
            value = Mathf.Clamp01(value);
            if (value * GlobalAlertness < float.Epsilon) {
                timeToHear = Mathf.Infinity;
            } else {
                timeToHear = timeToHear - 1 / (updatesPerSecond * hearingAlertness * GlobalAlertness) + 1 / (updatesPerSecond * value * GlobalAlertness); // subtract old adjustment and add new adjustment
            }
            hearingAlertness = value;
        }
    }
    
    [HideInInspector]
    [SerializeField]
    private float smellAlertness = 0;
    private float timeToSmell = 0;
    [ExposeProperty]
    public float SmellAlertness {
        get { return smellAlertness; }
        set {
            value = Mathf.Clamp01(value);
            if (value * GlobalAlertness < float.Epsilon) {
                timeToSmell = Mathf.Infinity;
            } else {
                timeToSmell = timeToSmell - 1 / (updatesPerSecond * smellAlertness * GlobalAlertness) + 1 / (updatesPerSecond * value * GlobalAlertness); // subtract old adjustment and add new adjustment
            }
            smellAlertness = value;
        }
    }

    [HideInInspector]
    [SerializeField]
    private float tasteAlertness = 0;
    private float timeToTaste = 0;
    [ExposeProperty]
    public float TasteAlertness {
        get { return tasteAlertness; }
        set {
            value = Mathf.Clamp01(value);
            if (value * GlobalAlertness < float.Epsilon) {
                timeToTaste = Mathf.Infinity;
            } else {
                timeToTaste = timeToTaste - 1 / (updatesPerSecond * tasteAlertness * GlobalAlertness) + 1 / (updatesPerSecond * value * GlobalAlertness); // subtract old adjustment and add new adjustment
            }
            tasteAlertness = value;
        }
    }

    [HideInInspector]
    [SerializeField]
    private float touchAlertness = 0;
    private float timeToFeel = 0;
    [ExposeProperty]
    public float TouchAlertness {
        get { return touchAlertness; }
        set {
            value = Mathf.Clamp01(value);
            if (value * GlobalAlertness < float.Epsilon) {
                timeToFeel = Mathf.Infinity;
            } else {
                timeToFeel = timeToFeel- 1 / (updatesPerSecond * touchAlertness * GlobalAlertness) + 1 / (updatesPerSecond * value * GlobalAlertness); // subtract old adjustment and add new adjustment
            }
            tasteAlertness = value;
        }
    }
    #endregion

    private float brainTickTime = 0;

	// Use this for initialization
	void Start () {
        emotion = GetComponent<Emotion>();
        memory = GetComponent<Memory>();
        eyes = GetComponent<Sight>();
        ears = GetComponent<Hearing>();
        nose = GetComponent<Smell>();
        tongue = GetComponent<Taste>();
        skin = GetComponent<Touch>();
        stomach = GetComponent<Hunger>();
        circadianRhythm = GetComponent<Chronoception>();
        mouth = GetComponent<Voice>();
        body = GetComponent<Motion>();
        
        // Needs to be some way of automating this process. So that it detects which components are available and those components indicate what skills they provide. Sort of like hardware and drivers.
        /*
        memory.longTermMemory.proceduralMemory.AddSkill(new Skill("Do Nothing", () => { mouth.Say("Idling"); }));
        memory.longTermMemory.proceduralMemory.AddSkill(new Skill("Cry", () => { mouth.Say("Crying"); }));
        memory.longTermMemory.proceduralMemory.AddSkill(new Skill("Blink", () => { mouth.Say("Blinking"); }));
        memory.longTermMemory.proceduralMemory.AddSkill(new Skill("Root", () => { mouth.Say("Rooting"); }));
        memory.longTermMemory.proceduralMemory.AddSkill(new Skill("Suck", () => { mouth.Say("Sucking"); }));
        memory.longTermMemory.proceduralMemory.AddSkill(new Skill("Moro", () => { mouth.Say("Moroing"); }));
        memory.longTermMemory.proceduralMemory.AddSkill(new Skill("Palmar Grasp", () => { mouth.Say("Grasping"); }));
        memory.longTermMemory.proceduralMemory.AddSkill(new Skill("Tonic Neck", () => { mouth.Say("Tonic Neck"); }));
        */
        /*
        if (mouth) {
            //memory.longTermMemory.proceduralMemory.Add(new Skill("Say Hello", mouth.SayHello));
        }
        if (body) {
            memory.longTermMemory.proceduralMemory.Add(new Skill("Move Forward", () => { mouth.Say("Moving Forward"); body.StartMovingForward(); }));
            memory.longTermMemory.proceduralMemory.Add(new Skill("Stop Moving", () => {mouth.Say("Stopping"); body.StopMoving();}));
            memory.longTermMemory.proceduralMemory.Add(new Skill("Move Backward", () => {mouth.Say("Moving Backward"); body.StartMovingBackward();}));
            memory.longTermMemory.proceduralMemory.Add(new Skill("Turn Left", () => { mouth.Say("Turning Left"); body.StartTurningLeft(); }));
            memory.longTermMemory.proceduralMemory.Add(new Skill("Stop Turning", () => { mouth.Say("Stopping"); body.StopTurning(); }));
            memory.longTermMemory.proceduralMemory.Add(new Skill("Turn Right", () => { mouth.Say("Turning Right"); body.StartTurningRight(); }));
            memory.longTermMemory.proceduralMemory.Add(new Skill("Jump", () => { mouth.Say("Jumping"); body.Jump(); }));
        }
        */
    }
	
	// Update is called once per frame
	void Update () {
        #region Sensing/Input Section
        // Sensing/Input section
        // Pull in information from the senses.
        if (eyes) {
            if (Time.time > timeToSee) {
                EmulateSightSense();
                timeToSee = Time.time + 1 / (updatesPerSecond * SightAlertness * GlobalAlertness);
            }
        }

        if (ears) {
            if (Time.time > timeToHear) {
                EmulateHearingSense();
                timeToHear = Time.time + 1 / (updatesPerSecond * HearingAlertness * GlobalAlertness);
            }
        }

        if (nose) {
            if (Time.time > timeToSmell) {
                // EmulateSmellSense();
                timeToSmell = Time.time + 1 / (updatesPerSecond * smellAlertness * GlobalAlertness);
            }
        }

        if (tongue) {
            if (Time.time > timeToTaste) {
                // EmulateTasteSense();
                timeToTaste = Time.time + 1 / (updatesPerSecond * tasteAlertness * GlobalAlertness);
            }
        }

        if (skin) {
            if (Time.time > timeToFeel) {
                // EmulateTouchSense();
                timeToFeel = Time.time + 1 / (updatesPerSecond * touchAlertness * GlobalAlertness);
            }
        }
        if (circadianRhythm) {
            
        }
         
        #endregion

        // Stuff that happens each brain tick
        if (Time.time > brainTickTime) {
            // Debug.Log("Tick");
            
            brainTickTime = Time.time + 1 / (updatesPerSecond * GlobalAlertness);

            #region Appraisal/Processing Section
            emotion.pad.Reset();
            // This is where initial reflex emotion state is calculated based solely on the sensed inputs.
            //if (memory.longTermMemory.episodicMemory.Count > 0) {
            //    Episode recentEpisode = memory.longTermMemory.episodicMemory[memory.longTermMemory.episodicMemory.Count - 1];
            //    foreach (VisualStimulus visualStimulus in memory.sensoryMemory.iconicMemory) {

            //    }
            //}

            // Structuralism Based Rules
            foreach (VisualStimulusData visualStimulus in memory.sensoryMemory.iconicMemory) {
                if (visualStimulus.brightness > 0) {
                    emotion.pad.Arousal += 1;
                }
            }

            foreach (AuralStimulusData auralStimulus in memory.sensoryMemory.echoicMemory) {
                if (auralStimulus.amplitude > 100) { // 100 hard-coded here for the threshold for fear.
                    // Good luck determining values for these numbers
                    emotion.pad.Arousal += 1;
                    emotion.pad.Valence += -1;
                    emotion.pad.Dominance += -1;
                }
            }

            // This is where emotional state is exaggerated or reduced due to previous experiences
            PADEmotion emotionSum = new PADEmotion();
            float links = 0;
            string fullDebug = "";
            // Debug.Log(memory.longTermMemory.episodicMemory.Count);
            foreach (Episode episode in memory.longTermMemory.episodicMemory) {
                Debug.Log(episode.visualStimuli.Count);
                foreach (VisualStimulusData visualStimulus in episode.visualStimuli) {
                    if (memory.sensoryMemory.iconicMemory.Contains<VisualStimulusData>(visualStimulus)) {
                        string debug;
                        float retention = CalculateMemoryRetention(episode, out debug);
                        // Debug.Log(visualStimulus.name + ": " + debug);
                        emotionSum = emotionSum + episode.emotionalState *retention;
                        links++;
                    } 
                }
                foreach (AuralStimulusData auralStimulus in episode.auralStimuli) {
                    if (memory.sensoryMemory.echoicMemory.Contains<AuralStimulusData>(auralStimulus)) {
                        fullDebug += auralStimulus;
                        string debug;
                        float retention = CalculateMemoryRetention(episode, out debug);
                        // Debug.Log(auralStimulus.name + ": " + debug);
                        emotionSum = emotionSum + episode.emotionalState * retention;
                        links++;
                    }
                }
            }
            if (links > 0) {
                PADEmotion emotionAverage = emotionSum / links;
                emotion.pad = (emotion.pad + emotionAverage) / 2;
                Debug.Log("Links: " + links + ", Sum: " + emotionSum + ", Average: " + emotionAverage + ", Current: " + emotion.pad);
            } else {
                // ToDo: Find out why links is 0 after seeing the mouse the second time.
                Debug.Log("Emotion: " + emotion.pad);
            }
            #endregion

            #region Storage section
            // This is where the sensory input as well as the appraisal modifications are stored into episodic memory
            // IEnumerable<ChronoStimulus> chronoStimuli = circadianRhythm.Sense() as IEnumerable<ChronoStimulus>;
            // memory.workingMemory.episodicBuffer.episode.time = chronoStimuli.First<ChronoStimulus>().time;
            memory.workingMemory.episodicBuffer.episode.time = Time.time;
            memory.workingMemory.episodicBuffer.episode.emotionalState = new PADEmotion(emotion.pad);
            memory.workingMemory.episodicBuffer.episode.visualStimuli = new List<VisualStimulusData>(memory.sensoryMemory.iconicMemory);
            memory.workingMemory.episodicBuffer.episode.auralStimuli = new List<AuralStimulusData>(memory.sensoryMemory.echoicMemory);
            memory.longTermMemory.episodicMemory.Add(new Episode(memory.workingMemory.episodicBuffer.episode));
            #endregion

            #region Behaviour/Output section
            // Now this is just hardcoded behaviour right into the brain. A bit high level to abstract away the complexities of actually emulating eating food.
            // However it does indicate a need for determining emergent behaviour building blocks.
            if (emotion.pad.Valence < -0.1f) { // crying threshold of -0.1
                if (!mouth.IsSpeaking) {
                    mouth.Say(Random.Range(0, mouth.clips.Count));
                }
            }

            
            // Action Selection is needed here somewhere.
            // int randomActionSelected = Random.Range(0, memory.longTermMemory.proceduralMemory.Count);
            // memory.longTermMemory.proceduralMemory[randomActionSelected].method();
            #endregion
        }

        

        

        /*
        // Maybe each sense should have their own timer and alertness level. Can allow for simulation of blind people with a sight alertness level of 0
        if (Time.time > timeToSense) {
            // Behaviour/Output section
            // Now this is just hardcoded behaviour right into the brain. A bit high level to abstract away the complexities of actually emulating eating food.
            // However it does indicate a need for determining emergent behaviour building blocks. Maybe procedural memory could come into play here as well.
            if ( memory.sensoryMemory.hapticMemory.Any( (HapticStimulus h)=>{ return (h.name == "Stimulus (Food)" ? true : false); } ) ) {
                stomach.Feed(5);
            }
            timeToSense = Time.time + 1/ (updatesPerSecond*GlobalAlertness);
        }
        */
	}

    void EmulateSightSense() {
        List<VisualStimulusData> visualStimuli = eyes.Sense();
        List<VisualStimulusData> newStimuli = visualStimuli.Except<VisualStimulusData>(memory.sensoryMemory.iconicMemory).ToList<VisualStimulusData>();
        // List<VisualStimulus> oldStimuli = memory.sensoryMemory.iconicMemory.Except<VisualStimulus>(visualStimuli).ToList<VisualStimulus>();
        memory.sensoryMemory.iconicMemory = visualStimuli;
        // Debug.Log("Seen New: " + newStimuli.Count + ", Current: " + visualStimuli.Count + ", Left: " + oldStimuli.Count);
        OnSeeNewStimuli(newStimuli);
        // OnLoseOldStimuli(oldStimuli);
    }

    void EmulateHearingSense() {
        List<AuralStimulusData> auralStimuli = ears.Sense();
        //List<AuralStimulus> newStimuli = auralStimuli.Except<AuralStimulus>(memory.sensoryMemory.echoicMemory).ToList<AuralStimulus>();
        //List<AuralStimulus> oldStimuli = memory.sensoryMemory.echoicMemory.Except<AuralStimulus>(auralStimuli).ToList<AuralStimulus>();
        memory.sensoryMemory.echoicMemory = auralStimuli;
    }
    /*
    void EmulateTouchSense() {
        List<HapticStimulus> hapticStimuli = skin.Sense();
        List<HapticStimulus> newStimuli = hapticStimuli.Except<HapticStimulus>(memory.sensoryMemory.hapticMemory).ToList<HapticStimulus>();
        // List<HapticStimulus> oldStimuli = memory.sensoryMemory.hapticMemory.Except<HapticStimulus>(hapticStimuli).ToList<HapticStimulus>();
        memory.sensoryMemory.hapticMemory = hapticStimuli;
        // Debug.Log("Feel New: " + newStimuli.Count + ", Current: " + hapticStimuli.Count + ", Left: " + oldStimuli.Count);
        OnFeelNewStimuli(newStimuli);
    }

    void EmulateSmellSense() {
        List<OlfacticStimulus> olfacticStimuli = nose.Sense();
        //List<OlfacticStimulus> newStimuli = olfacticStimuli.Except<OlfacticStimulus>(memory.sensoryMemory.olfacticMemory).ToList<OlfacticStimulus>();
        //List<OlfacticStimulus> oldStimuli = memory.sensoryMemory.olfacticMemory.Except<OlfacticStimulus>(olfacticStimuli).ToList<OlfacticStimulus>();
        memory.sensoryMemory.olfacticMemory = olfacticStimuli;
        // Debug.Log("Smell New: " + newStimuli.Count + ", Current: " + olfacticStimuli.Count + ", Left: " + oldStimuli.Count);
    }

    void EmulateTasteSense() {
        List<GusticStimulus> gusticStimuli = tongue.Sense();
    }
*/
    void OnSeeNewStimuli(List<VisualStimulusData> newStuff) {
        foreach (VisualStimulusData stuff in newStuff) {
            if (stuff.brightness > 1) {
                // Sensory interrupt
            }
            // Pull the game object into working memory
            // But Why?
            // Attention/Focus should command what gets pulled into short term memory.
            // But attention needs to be motivated 
            // I also need to figure out a representation of the contents of the visuospatial sketchpad other than just a List<GameObject>
            //memory.workingMemory.visuospatialSketchpad.Add(stuff.gameObject);
            
            emotion.pad.Arousal = 1; // Novelty?? Yeah novelty would affect arousal. 
            // The novelty would be higher the first time but decrease each subsequent tick. 
            // Then there's other factors which affect arousal. 
            // Contrast over time (Motion), Contrast in colour (Edge Detection) Did you know newborn babies initially see in grayscale?
        }
    }

    void OnLoseOldStimuli(List<VisualStimulusData> oldStuff) {
        //foreach (VisualStimulus stuff in oldStuff) {
            // Now what?
        //}
    }

    

    void OnFeelNewStimuli(List<HapticStimulus> newStuff) {
        //foreach (HapticStimulus stuff in newStuff) {
        //}
    }

    

    void EmulateHungerSense() {
        float hungerValue = stomach.Sense();
        // hard coding how an agent should feel if hungry. Not sure this actually belongs here. More likely should be in the appraisal section
        emotion.pad.Valence = -hungerValue / stomach.hungerLimit;
    }

    float CalculateMemoryRetention(Episode episode, out string debug) {
        // Ebbinghaus Forgetting Curve R = e^(-t/S)
        float time = Time.time - episode.time;
        float strength = episode.emotionalState.Arousal*100; // Let the PAD arousal be the strength for now, though I think part of the other factors play a part as well.
        float retention = Mathf.Exp(-time / strength);
        debug = "e ^ (-" + time + "/" + strength + ") = " + retention;
        return retention;
    }

    float CalculateMemoryRetention(Episode episode) {
        string debug;
        return CalculateMemoryRetention(episode, out debug);
    }

    void MoveTowards(Transform target) {
        GetComponent<Rigidbody>().AddForce((target.position - transform.position).normalized, ForceMode.Force);
    }
}