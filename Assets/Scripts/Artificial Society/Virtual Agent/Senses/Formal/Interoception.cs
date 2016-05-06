using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Stimuli;
using Memories;

namespace Senses {
    /// <summary>
    /// Interoception is any sense that is normally stimulated from within the body.
    /// </summary>
    /// <remarks>
    /// <para>Some examples of specific receptors are:</para>
    /// <list type="number">
    /// <item><description>Hunger (motivational state)</description></item>
    /// <item><description>Pulmonary stretch receptors are found in the lungs and control the respiratory rate.</description></item>
    /// <item><description>Peripheral chemoreceptors in the brain monitor the carbon dioxide and oxygen levels in the brain to give a feeling of suffocation if carbon dioxide levels get too high.</description></item>
    /// <item><description>The chemoreceptor trigger zone is an area of the medulla in the brain that receives inputs from blood-borne drugs or hormones, and communicates with the vomiting center.</description></item>
    /// <item><description>Chemoreceptors in the circulatory system also measure salt levels and prompt thirst if they get too high; they can also respond to high sugar levels in diabetics.</description></item>
    /// <item><description>Cutaneous receptors in the skin not only respond to touch, pressure, and temperature, but also respond to vasodilation in the skin such as blushing.</description></item>
    /// <item><description>Stretch receptors in the gastrointestinal tract sense gas distension that may result in colic pain.</description></item>
    /// <item><description>Stimulation of sensory receptors in the esophagus result in sensations felt in the throat when swallowing, vomiting, or during acid reflux.</description></item>
    /// <item><description>Sensory receptors in pharynx mucosa, similar to touch receptors in the skin, sense foreign objects such as food that may result in a gag reflex and corresponding gagging sensation.</description></item>
    /// <item><description>Stimulation of sensory receptors in the urinary bladder and rectum may result in sensations of fullness.</description></item>
    /// <item><description>Stimulation of stretch sensors that sense dilation of various blood vessels may result in pain, for example headache caused by vasodilation of brain arteries.</description></item>
    /// </list>
    /// </remarks>
    public class Interoception : AbstractSense {
        public override void Sense(SensoryMemory sensoryMemoryRef) {
            return;
        }
    }
}