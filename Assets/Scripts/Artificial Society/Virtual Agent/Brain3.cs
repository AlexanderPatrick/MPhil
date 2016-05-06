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
using System;

public class Brain3 : MonoBehaviour {
    public float updatesPerSecond = 1;
    public float noveltyStrength = 1;
    public float boredomUpperLimit = 0.1f;
    public float boredomLowerLimit = 0.01f;

    private Emotion emotion;
    private Memory memory;
    private string behaviour = "Do Nothing";
    private List<string> plan;

    public ActionCounter actionCounter;

    // Sensors
    private Sight eyes;
    private Hearing ears;
    private Touch skin;
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
            touchAlertness = value;
        }
    }
    #endregion

    private float brainTickTime = 0;
    private int tickCounter = 0;

    private float motivationThreshold = 1;
    

	// Use this for initialization
	void Start () {
        emotion = GetComponent<Emotion>();
        memory = GetComponent<Memory>();
        plan = new List<string>();
        eyes = GetComponent<Sight>();
        ears = GetComponent<Hearing>();
        skin = GetComponent<Touch>();
        circadianRhythm = GetComponent<Chronoception>();
        mouth = GetComponent<Voice>();
        body = GetComponent<Motion>();
        
        memory.longTermMemory.proceduralMemory.AddSkill(new Skill("Do Nothing", () => { },true));
        /*
        memory.longTermMemory.proceduralMemory.Add(new Skill("Cry", () => { mouth.Say("Crying"); }));
        memory.longTermMemory.proceduralMemory.Add(new Skill("Blink", () => { mouth.Say("Blinking"); }));
        memory.longTermMemory.proceduralMemory.Add(new Skill("Root", () => { mouth.Say("Rooting"); }));
        memory.longTermMemory.proceduralMemory.Add(new Skill("Suck", () => { mouth.Say("Sucking"); }));
        memory.longTermMemory.proceduralMemory.Add(new Skill("Moro", () => { mouth.Say("Moroing"); }));
        memory.longTermMemory.proceduralMemory.Add(new Skill("Palmar Grasp", () => { mouth.Say("Grasping"); }));
        memory.longTermMemory.proceduralMemory.Add(new Skill("Tonic Neck", () => { mouth.Say("Tonic Neck"); }));
        */
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > brainTickTime) {
            brainTickTime = Time.time + 1 / updatesPerSecond;
            string debugString = "Tick #" + ++tickCounter;
            
            #region Sensing/Input Section
            if (eyes) See();
            if (ears) Hear();
            if (skin) Feel();
            PADEmotion previousEmotion = new PADEmotion(emotion.pad);
            string previousBehaviour = behaviour;
            #endregion

            #region Appraisal/Processing Section
            // Automatic decay
            emotion.pad.Decay(1 / updatesPerSecond, 1);

            // Structuralism Based Rules 
            // And the biggest problem with my entire thesis 
            // because I don't have any research which says what these rules are. 
            // Everything here is assumed. 
            // Only justification being unconditioned stimulus response
            foreach (VisualStimulusData visualStimulus in memory.sensoryMemory.iconicMemory) {
                if (visualStimulus.brightness > 0) {
                    emotion.pad.Arousal += 1;
                }
            }

            foreach (VisualStimulusData visualStimulus in memory.sensoryMemory.iconicMemory) {
                if (visualStimulus.brightness >= 100) {
                    emotion.pad.Arousal += 1;
                    emotion.pad.Valence += -1f;
                    emotion.pad.Dominance += -1;
                }
            }

            foreach (AuralStimulusData auralStimulus in memory.sensoryMemory.echoicMemory) {
                if (auralStimulus.amplitude > 100) {
                    emotion.pad.Arousal += 1;
                    emotion.pad.Valence += -1;
                    emotion.pad.Dominance += -1;
                }
            }

            foreach (AuralStimulusData auralStimulus in memory.sensoryMemory.echoicMemory) {
                if (auralStimulus.amplitude == 50) {
                    emotion.pad.Arousal += 1;
                    emotion.pad.Valence += .1f;
                    emotion.pad.Dominance += 1;
                }
            }

            // This is where emotional state is exaggerated or reduced due to previous experiences
            // Sum all past memories with similar stimuli scaled down by how far into the past the memories were.
            PADEmotion emotionSum = new PADEmotion();
            float links = 0;
            foreach (Episode episode in memory.longTermMemory.episodicMemory) {
                foreach (VisualStimulusData visualStimulus in episode.visualStimuli) {
                    if (memory.sensoryMemory.iconicMemory.Contains<VisualStimulusData>(visualStimulus)) {
                        string debug;
                        float retention = CalculateMemoryRetention(episode, out debug);
                        emotionSum = emotionSum + episode.emotionalState * retention;
                        links++;
                    } 
                }
                foreach (AuralStimulusData auralStimulus in episode.auralStimuli) {
                    if (memory.sensoryMemory.echoicMemory.Contains<AuralStimulusData>(auralStimulus)) {
                        string debug;
                        float retention = CalculateMemoryRetention(episode, out debug);
                        emotionSum = emotionSum + episode.emotionalState * retention;
                        links++;
                    }
                }
            }
            if (links > 0) {
                PADEmotion emotionAverage = emotionSum / links;
                emotion.pad = (emotion.pad + emotionAverage) / 2;
            }
            PADEmotion emotionDelta = emotion.pad - previousEmotion;

            debugString += "\nEmotion: " + previousEmotion + " -> " + emotion.pad;
            #endregion

            #region Pseudo-Learning section
            if (tickCounter == 1) memory.longTermMemory.proceduralMemory.AddSkill(new Skill("Turn On Light", () => {
                GameObject.Find("Bright Light").GetComponent<LightSwitch>().TurnOn();
                memory.longTermMemory.proceduralMemory.FindSkill("Press Button").available = true;
                memory.longTermMemory.proceduralMemory.FindSkill("Turn Off Light").available = true;
                memory.longTermMemory.proceduralMemory.FindSkill("Turn On Light").available = false;
            },true));
            if (tickCounter == 1) memory.longTermMemory.proceduralMemory.AddSkill(new Skill("Turn Off Light", () => {
                GameObject.Find("Bright Light").GetComponent<LightSwitch>().TurnOff();
                memory.longTermMemory.proceduralMemory.FindSkill("Press Button").available = false;
                memory.longTermMemory.proceduralMemory.FindSkill("Turn Off Light").available = false;
                memory.longTermMemory.proceduralMemory.FindSkill("Turn On Light").available = true;
            },false));
            if (tickCounter == 1) memory.longTermMemory.proceduralMemory.AddSkill(new Skill("Press Button", () => {
                bool paperFound = false;
                foreach (VisualStimulusData datum in memory.sensoryMemory.iconicMemory) {
                    if (datum.shape.name == "Cylinder Instance") {
                        paperFound = true;
                        break;
                    }
                }

                if (paperFound) {
                    GameObject.Find("Pleasant Sound").GetComponent<SphereCollider>().radius = 10;
                    GameObject.Find("Pleasant Sound").GetComponent<AudioSource>().Play();
                }
            },false));
            // if (tickCounter == 10) memory.longTermMemory.proceduralMemory.Add(new Skill("Smile", () => { /*GetComponent<Renderer>().material.color = new Color(1, 1, 0);*/ }));
            // if (tickCounter == 20) memory.longTermMemory.proceduralMemory.Add(new Skill("Cry", () => { /*GetComponent<Renderer>().material.color = new Color(0, 0, 1);*/ }));
            #endregion

            #region Behaviour/Output section
            Dictionary<List<string>, SkillAffinity> planAffinity = new Dictionary<List<string>, SkillAffinity>();
            if (plan.Count == 0) {
                // We have no plan, so make a plan or perform a random action

                if (UnityEngine.Random.Range(0, 10) > 9) {
                    Debug.Log("Planning");
                    // Need to make it a plan (list of skills) so that sequential behaviours are monitored 
                    int planDepth = 2;
                    /*
                    for (int i = 0; i < planDepth; i++) {
                        for (int j = 0; j <= i; j++) {
                            List<string> behaviourSequence = new List<string>();
                            for (int k = 0; k < memory.longTermMemory.proceduralMemory.Count; k++) {
                                behaviourSequence.Add(memory.longTermMemory.episodicMemory[j - k].previousBehaviour);
                            }
                        }
                    }
                    */
                    for (int i = 0; i < planDepth; i++) {
                        for (int j = memory.longTermMemory.episodicMemory.Count - 1; j >= i; j--) {
                            List<string> behaviourSequence = new List<string>();
                            PADEmotion padSum = new PADEmotion();
                            for (int k = i; k >= 0; k--) {
                                behaviourSequence.Add(memory.longTermMemory.episodicMemory[j - k].previousBehaviour);
                                padSum += memory.longTermMemory.episodicMemory[j - k].emotionDelta;
                            }
                            bool containsKey = false;
                            List<string> matchingKey = null;
                            foreach (var key in planAffinity.Keys) {
                                if (key.SequenceEqual<string>(behaviourSequence)) {
                                    containsKey = true;
                                    matchingKey = key;
                                    break;
                                }
                            }
                            if (!containsKey) {
                                matchingKey = behaviourSequence;
                                planAffinity.Add(matchingKey, new SkillAffinity());
                                planAffinity[matchingKey].Novelty = -1;
                            }
                            planAffinity[matchingKey].Valence += padSum.valence;

                            // if statement and dummy -1 value are because the j iterator goes from most recent to oldest and we want to end up with the most recent occurrence
                            if (planAffinity[matchingKey].Novelty == -1) {
                                // t is the time since last performed the plan.
                                float t = Time.time - memory.longTermMemory.episodicMemory[j].time;
                                // Novelty = 1 - e ^ (-t / S)
                                planAffinity[matchingKey].Novelty = 1 - Mathf.Exp(-t / noveltyStrength);
                            }

                            planAffinity[matchingKey].Boredom += boredomLowerLimit + (boredomUpperLimit - boredomLowerLimit) * j / memory.longTermMemory.episodicMemory.Count;
                        }
                    }
                }


                if (planAffinity.Count > 0) {
                    // find the most positive value
                    float bestMotivationValue = planAffinity.Max((KeyValuePair<List<string>, SkillAffinity> kvp) => { return kvp.Value.Valence + kvp.Value.Novelty - kvp.Value.Boredom; });
                                            
                    // find the actions with the best positive value
                    List<List<string>> bestPlans = new List<List<string>>();
                    foreach (var item in planAffinity) {
                        if (planAffinity[item.Key].Valence + planAffinity[item.Key].Novelty - planAffinity[item.Key].Boredom >= bestMotivationValue - motivationThreshold) {
                            bestPlans.Add(item.Key);
                        }
                    }

                    // select random action from bestActions
                    int selectedIndex = UnityEngine.Random.Range(0, bestPlans.Count - 1);
                    plan = bestPlans[selectedIndex];
                } else {
                    // no prior experiences or not planning, so random action
                    int selectedIndex = UnityEngine.Random.Range(0, memory.longTermMemory.proceduralMemory.AvailableSkills.Count);
                    plan.Add(memory.longTermMemory.proceduralMemory.AvailableSkills[selectedIndex].name);
                }
            }

            string action = plan[0];
            plan.RemoveAt(0);

            int bestActionIndex = 0;
            foreach (Skill skill in memory.longTermMemory.proceduralMemory) {
                if (action == skill.name) {
                    break;
                }
                bestActionIndex++;
            }

            behaviour = memory.longTermMemory.proceduralMemory[bestActionIndex].name;
            memory.longTermMemory.proceduralMemory[bestActionIndex].method();
            memory.longTermMemory.proceduralMemory[bestActionIndex].novelty = 0;

            switch (behaviour) {
                case "Do Nothing": actionCounter.nothing++; break;
                case "Turn On Light": actionCounter.lighton++; break;
                case "Turn Off Light": actionCounter.lightoff++; break;
                case "Press Button": actionCounter.pressbutton++; break;
            }
            
            // Calculate Novelty and Boredom values for (the action?)


            debugString += "\tAction: " + previousBehaviour + " -> " + behaviour;
            foreach (var item in planAffinity) {
                debugString += "\n";
                foreach (var item2 in item.Key) {
                    debugString += item2 + ",";
                }
                debugString += " = " + item.Value;
            }
            #endregion

            #region Storage section
            memory.workingMemory.episodicBuffer.episode.time = Time.time;
            memory.workingMemory.episodicBuffer.episode.visualStimuli = new List<VisualStimulusData>(memory.sensoryMemory.iconicMemory);
            memory.workingMemory.episodicBuffer.episode.auralStimuli = new List<AuralStimulusData>(memory.sensoryMemory.echoicMemory);
            memory.workingMemory.episodicBuffer.episode.hapticStimuli = new List<HapticStimulusData>(memory.sensoryMemory.hapticMemory);
            memory.workingMemory.episodicBuffer.episode.previousEmotionalState = previousEmotion;
            memory.workingMemory.episodicBuffer.episode.emotionDelta = emotionDelta;
            memory.workingMemory.episodicBuffer.episode.previousBehaviour = previousBehaviour;
            memory.workingMemory.episodicBuffer.episode.emotionalState = new PADEmotion(emotion.pad);
            memory.workingMemory.episodicBuffer.episode.behaviour = behaviour;
            
            memory.longTermMemory.episodicMemory.Add(new Episode(memory.workingMemory.episodicBuffer.episode));
            #endregion

            Debug.Log(debugString);
        }
	}

    void See() {
        List<VisualStimulusData> visualStimuli = eyes.Sense();
        memory.sensoryMemory.iconicMemory = visualStimuli;
    }

    void Hear() {
        List<AuralStimulusData> auralStimuli = ears.Sense();
        memory.sensoryMemory.echoicMemory = auralStimuli;
    }

    void Feel() {
        List<HapticStimulusData> hapticStimuli = skin.Sense();
        memory.sensoryMemory.hapticMemory = hapticStimuli;
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

    class SkillAffinity {
        public float Valence { get; set; }
        public float Novelty { get; set; }
        public float Boredom { get; set; }
    }
}