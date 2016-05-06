using UnityEngine;
using System.Collections;

namespace NPC {
	public class Motor : MonoBehaviour {
		public Transform target;
		public float speed;
		public float threshold;
		
		public bool ignoreElevation;
		private CharacterController characterController;
			
		// Use this for initialization
		void Start() {
			characterController = gameObject.GetComponent<CharacterController>();
			if (!characterController) {
				this.enabled = false;
			}
		}
		
		// Update is called once per frame
		void Update() {
			if (target) {
				Vector3 direction = target.position - transform.position;
				if (ignoreElevation) direction = new Vector3(direction.x, 0, direction.z);
				float distance = direction.magnitude;
				if (distance > threshold) {
					characterController.Move(direction.normalized * speed * Time.deltaTime);
				} else {
					target = null;
				}
			}
		}
	}
}