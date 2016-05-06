using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Stimuli;
using Memories;

namespace Senses {
    /// <summary>
    /// Gustation refers to the capability to detect the taste of substances such as food, certain minerals, and poisons, etc.
    /// </summary>
    /// <remarks>
    /// The sense of taste is often confused with the "sense" of flavor, which is a combination of taste and smell perception. 
    /// Flavor depends on odor, texture, and temperature as well as on taste.
    /// Humans receive tastes through sensory organs called taste buds, or gustatory calyculi, concentrated on the upper surface of the tongue. 
    /// There are five basic tastes: sweet, bitter, sour, salty and umami. 
    /// Other tastes such as calcium and free fatty acids may also be basic tastes but have yet to receive widespread acceptance. 
    /// The inability to taste is called ageusia.
    /// </remarks>
    public class Gustation : AbstractSense {
        public override void Sense(SensoryMemory sensoryMemoryRef) {
            return;
        }
    }
}