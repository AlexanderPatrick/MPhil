using UnityEngine;

namespace Senses {
    /// <summary>
    /// Chronoception refers to how the passage of time is perceived and experienced.
    /// 
    /// Current Implementation: 
    ///     Returns the amount of time passed since the simulation began.
    ///     
    /// Current Drawbacks: 
    ///     All agents have the same time resolution. 
    ///     No accounting for individual agents' varying perception of time. (Time Dilation)
    /// 
    /// Possible improvements: 
    ///     It would be good if this was done with each agent having their own individual pulses and the pulse rate is affected by the heart rate/emotional impact of the event.
    ///     A Computer-based analogy would be differing computers varying clock rates or Frame rates. 
    ///     
    /// It would be good to be able to account for these. https://en.wikipedia.org/wiki/Time_perception#Types_of_temporal_illusions
    /// </summary>
    public class Chronoception : MonoBehaviour {
        public float Sense() {
            return Time.time;
        }
    }
}