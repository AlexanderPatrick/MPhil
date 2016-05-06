using UnityEngine;
using System.Collections;

namespace Nerves {
    public class SomaticNervousSystem : MonoBehaviour {
        SomaticNervousSystemSegment[] spinalSegments;
        SomaticNervousSystemSegment[] cranialSegments;
        Nerve[] associationNerves;

        void Start() {
            // There are 43 segments of nerves in the human body.
            // In the body, 31 segments of nerves are in the spinal cord
            spinalSegments = new SomaticNervousSystemSegment[31];
            // and 12 are in the brain stem.
            cranialSegments = new SomaticNervousSystemSegment[12];
            // thousands of association nerves are also present in the body.
            associationNerves = new Nerve[1000];
        }

        void Update() {

        }

        private class SomaticNervousSystemSegment {
            // With each segment, there is a pair of sensory and motor nerves.
            SensoryNerves sensoryNerves;
            MotorNerves motorNerves;

            private class SensoryNerves {
            }

            private class MotorNerves {
            }
        }

        private class Nerve {
        }
    }
}