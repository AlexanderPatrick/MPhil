using UnityEngine;
using Stimuli;

namespace Senses {
    /// <summary>
    /// Audition is the sense of sound perception.
    /// </summary>
    public class Audition : MonoBehaviour {
        private AuralStimulusData buffer;

        public void Hear(AuralStimulus auralStimulus) {
            buffer = new AuralStimulusData();
            float distance = Vector3.Distance(auralStimulus.Position, transform.position);
            buffer.amplitude = auralStimulus.Amplitude * distance / auralStimulus.Amplitude;
            buffer.frequency = auralStimulus.Frequency; // Maybe I should get rid of this
            buffer.position = auralStimulus.Position; // Let's assume for now that listeners have perfect aural positioning. It might actually be true if not accounting for echo and rebounding off of walls.
        }

        public void Sense() {
            return;
        }
    }
}