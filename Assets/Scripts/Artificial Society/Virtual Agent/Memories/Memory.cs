using UnityEngine;
using System;
using System.Collections.Generic;

namespace Memories {
    public class Memory : MonoBehaviour {
        public SensoryMemory sensoryMemory;
        public WorkingMemory workingMemory;
        public LongTermMemory longTermMemory;

        void Awake() {
            sensoryMemory = new SensoryMemory();
            workingMemory = new WorkingMemory();
            longTermMemory = new LongTermMemory();
        }
    }
}