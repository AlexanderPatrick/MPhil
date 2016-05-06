using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Memories {
    [Serializable]
    public class LongTermMemory {
        public List<SemanticMemory> semanticMemory; // Knowledge Memory
        public List<Episode> episodicMemory; // History Memory
        // public List<Skill> proceduralMemory; // Skills Memory
        public ProceduralMemory proceduralMemory;
        public List<ProspectiveMemory> prospectiveMemory; // Goal Memory

        public LongTermMemory() {
            semanticMemory = new List<SemanticMemory>();
            episodicMemory = new List<Episode>();
            // proceduralMemory = new List<Skill>();
            proceduralMemory = new ProceduralMemory();
            prospectiveMemory = new List<ProspectiveMemory>();
        }

        public int SkillNameToIndex(string name) {
            for (int i = 0; i < proceduralMemory.Count; i++) {
                if (proceduralMemory[i].name == name) {
                    return i;
                }
            }
            Debug.LogError(name + " is not a known skill.");
            return -1;
        }
    }
}