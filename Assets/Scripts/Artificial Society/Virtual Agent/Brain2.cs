using UnityEngine;
using System.Collections;
using Senses;
using Memories;
using Behaviours;
using Motion = Behaviours.Motion;

namespace VirtualAgent {
    public class Brain2 : MonoBehaviour {

        // Senses
        public Chronoception circadianRhythm;
        public Vision eyes;
        public Audition ears;
        public Gustation tongue;
        public Olfaction nose;
        public Somatosensation touch;
        public Thermoception temp;
        public Nociception pain;
        public Equilibrioception balance;
        public Proprioception body;
        public Interoception physiology;

        // Memories
        public Memory memory;

        // Behaviours
        public Voice mouth;
        public Motion legs;

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }
    }
}