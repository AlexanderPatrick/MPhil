using UnityEngine;
using System.Collections;

public class PartialVisibility : MonoBehaviour {
	public Transform target;
	public bool isVisible;
	public bool isFullyVisible;
	void Update () {
		MeshFilter mf = target.GetComponent<MeshFilter>();
		Vector3[] vertices = mf.mesh.vertices;
		Vector3 source = transform.position;
		isVisible = false;
		isFullyVisible = true;
		foreach (Vector3 vertex in vertices) {
			Vector3 direction = target.position + vertex - source;
//			Debug.DrawRay(source, direction, Color.white);
			Ray ray = new Ray(source, direction);
			RaycastHit hitInfo;
			if ( Physics.Raycast(ray, out hitInfo) ) {
				Debug.DrawRay(source, hitInfo.point-source, Color.white);
				if ( hitInfo.transform.Equals(target) ) {
					isVisible = true;
				} else {
					isFullyVisible = false;
				}
			}
		}
		
		if (isFullyVisible) {
			Debug.Log("Target is fully visible.");
		} else if (isVisible) {
			Debug.Log("Target is in line of sight.");
		} else {
			Debug.Log("Target is not in line of sight.");
		}
	}
}