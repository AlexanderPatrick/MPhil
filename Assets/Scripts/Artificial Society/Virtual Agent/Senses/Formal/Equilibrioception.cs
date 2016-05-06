using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Stimuli;
using Memories;

namespace Senses {
    /// <summary>
    /// Equilibrioception is the sense that allows an organism to sense body movement, direction, and acceleration, and to attain and maintain postural equilibrium and balance.
    /// It is managed by three semi-circular canals in the ear. 
    /// 
    /// Current Implementation:
    ///     Returns the angle between the transform's local down vector and gravity's vector.
    ///     Lacks the tracking of body movement, direction
    /// 
    /// </summary>
    public class Equilibrioception : MonoBehaviour {
        public float Sense() {
            float angle = Vector3.Angle(Physics.gravity.normalized, -transform.up);
            return angle;
        }
    }
}