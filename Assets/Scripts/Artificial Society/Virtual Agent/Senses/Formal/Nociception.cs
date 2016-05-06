using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Stimuli;
using Memories;

namespace Senses {
    /// <summary>
    /// Nociception (physiological pain) signals nerve-damage or damage to tissue. 
    /// The three types of pain receptors are cutaneous (skin), somatic (joints and bones), and visceral (body organs).
    /// </summary>
    public class Nociception : AbstractSense {
        public override void Sense(SensoryMemory sensoryMemoryRef) {
            return;
        }
    }
}