using UnityEngine;
using System;
using System.Collections;

namespace NPC {
	[Serializable]
	public class Location {
		public string locationString;
		
		// absolute positioning
		public Vector3 absolutePosition;
		
		// relative
		public GameObject relativeObject;
		public Vector3 relativeObjectBounds;
		public Vector3 relativePosition;
		
		// Default Constructor
		public Location() {
			locationString = "";
			absolutePosition = Vector3.zero;
			relativePosition = Vector3.zero;
			relativeObject = null;
			relativeObjectBounds = Vector3.zero;
		}
		
		public Location(GameObject gameObject) {
			locationString = "";
			if (gameObject && gameObject.transform) {
				if (gameObject.transform.parent) {
					relativeObject = gameObject.transform.parent.gameObject;
					if (relativeObject.GetComponent<Collider>()) {
						relativeObjectBounds = relativeObject.GetComponent<Collider>().bounds.size;
					}
					relativePosition = gameObject.transform.position;
				} else {
					absolutePosition = gameObject.transform.position;
				}
			}
			buildLocationString();
		}
		
		void buildLocationString() {
			if (relativeObject) {
				if (relativeObjectBounds != Vector3.zero) {
					if (relativePosition.x <= relativeObjectBounds.x && relativePosition.x >= 0 && relativePosition.y <= relativeObjectBounds.y && relativePosition.y >= 0 && relativePosition.z <= relativeObjectBounds.z && relativePosition.z >= 0) {
						locationString = "in " + relativeObject.name;
					} else if (relativePosition.x <= relativeObjectBounds.x && relativePosition.x >= 0 && relativePosition.y > relativeObjectBounds.y && relativePosition.z <= relativeObjectBounds.z && relativePosition.z >= 0) {
						locationString = "over " + relativeObject.name;
					} else if (relativePosition.x <= relativeObjectBounds.x && relativePosition.x >= 0 && relativePosition.y < 0 && relativePosition.z <= relativeObjectBounds.z && relativePosition.z >= 0) {
						locationString = "under " + relativeObject.name;
					}
				} else {
					if (relativePosition.x == 0 && relativePosition.y == 0 && relativePosition.z == 0) {
						locationString = "at " + relativeObject.name;
					} else {
						
					} 
				}
			}
		}
	}
}
