using UnityEngine;
using System.Collections;

public class LineOfSight : MonoBehaviour {
	public Transform target;
	public bool inSight = false;
	void Update () {
		Vector3 source = transform.position;
		Vector3 direction = target.position - source;
		Ray ray = new Ray(source, direction);
		RaycastHit hitInfo;
		if ( Physics.Raycast(ray, out hitInfo) ) {
			if ( hitInfo.transform.Equals(target) ) {
				Debug.Log("Target is in line of sight.");
				inSight = true;
			} else {
				Debug.Log("Target is not in line of sight.");
				inSight = false;
			}
		}
		Debug.DrawRay(source, direction, Color.white);
	}
}