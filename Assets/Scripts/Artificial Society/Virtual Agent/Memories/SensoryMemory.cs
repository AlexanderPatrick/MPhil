using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Stimuli;

namespace Memories {
    [Serializable]
    public class SensoryMemory {
        public List<VisualStimulusData> iconicMemory;
        public List<AuralStimulusData> echoicMemory;
        public List<OlfacticStimulusData> olfacticMemory;
        public List<ThermalStimulusData> thermalMemory;
        public List<HapticStimulusData> hapticMemory;
        public List<GusticStimulusData> gusticMemory;
        public List<ChronoStimulusData> chronoMemory;
        

        public SensoryMemory() {
            iconicMemory    = new List<VisualStimulusData>();
            echoicMemory    = new List<AuralStimulusData>();
            olfacticMemory  = new List<OlfacticStimulusData>();
            thermalMemory   = new List<ThermalStimulusData>();
            hapticMemory    = new List<HapticStimulusData>();
            gusticMemory    = new List<GusticStimulusData>();
            chronoMemory    = new List<ChronoStimulusData>();
        }
    }
}