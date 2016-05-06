using UnityEngine;
using System.Collections;

public class Location : MonoBehaviour {

	public string LocationString;
	
	// absolute
	public float absoluteX, absoluteY, absoluteZ;
	
	// relative
	public float relativeX, relativeY, relativeZ;
	public GameObject parent;
	
	void Start() {
		if (transform) {
			absoluteX = transform.position.x;
			absoluteY = transform.position.y;
			absoluteZ = transform.position.z;
		}
	}
}
