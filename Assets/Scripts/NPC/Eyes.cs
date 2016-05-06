using UnityEngine;
using System.Collections;

namespace NPC {
	/// <summary>
	/// Captures the 
	/// </summary>
	public class Eyes : MonoBehaviour {
		public NPC npc;
		
		void Start() {
			if (!npc) {
				Debug.Log("The eyes are not connected to any NPC.", npc);
				this.enabled = false;
			}
		}
		
		void OnTriggerEnter(Collider visualStimulus) {
			npc.canSee(visualStimulus);
		}
		
		void OnTriggerExit(Collider visualStimulus) {
		
		}
	}
}