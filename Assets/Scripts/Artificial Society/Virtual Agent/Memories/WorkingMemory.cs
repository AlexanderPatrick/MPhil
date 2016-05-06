using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Memories {
    [Serializable]
    public class WorkingMemory {
        public List<GameObject> visuospatialSketchpad;
        public List<GameObject> phonologicalLoop;
        public EpisodicBuffer episodicBuffer;

        public WorkingMemory() {
            visuospatialSketchpad = new List<GameObject>();
            phonologicalLoop = new List<GameObject>();
            episodicBuffer = new EpisodicBuffer();
        }
    }
}