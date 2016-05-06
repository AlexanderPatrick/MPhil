using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Memories;

namespace Senses {
    /// <summary>
    /// Abstract Sense class for common functionality.
    /// </summary>
    public abstract class AbstractSense : MonoBehaviour {
        /// <summary>
        /// Sense the environment
        /// </summary>
        public abstract void Sense(SensoryMemory sensoryMemoryRef); 
    }
}