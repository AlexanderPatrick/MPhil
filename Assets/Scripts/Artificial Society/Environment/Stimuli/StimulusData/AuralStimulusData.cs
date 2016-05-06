using UnityEngine;
using System;

namespace Stimuli {
    [Serializable]
    public struct AuralStimulusData : IEquatable<AuralStimulusData> {
        public float frequency;
        public float amplitude;
        public Vector3 position;

        public override string ToString() {
            return "(Amplitude: " + amplitude + "Position: " + position + ")";
        }

        public bool Equals(AuralStimulusData other) {
            return (frequency == other.frequency && amplitude == other.amplitude && position == other.position);
        }
    }
}