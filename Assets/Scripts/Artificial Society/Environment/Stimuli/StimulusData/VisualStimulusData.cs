using UnityEngine;
using System;
using System.Collections;

namespace Stimuli {
    [Serializable]
    public struct VisualStimulusData {
        public Vector3 position;
        public Color colour;
        public Mesh shape;
        public float brightness;
    }
}