using UnityEngine;
using System.Collections;

namespace Stimuli {
    // GRAVITY!!!
    public class EquilibrioStimulus : MonoBehaviour {
        public Vector3 centerOfMass; // Most likely the transform position
        public float radius; // Most likely the radius of the sphere
        // It also has to have a trigger indicating to all rigidbodies that it will have an effect on it.
        public float gravitationalRadius; // apparently this value is actually infinite but decreases to a point that it is negligible. Gotta find a negligible threshold then.

    }
}