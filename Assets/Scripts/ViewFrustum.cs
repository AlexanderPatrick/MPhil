using UnityEngine;
using System.Collections;

public class ViewFrustum : MonoBehaviour {
	public GameObject target;
	private Camera eyes;
	private Plane[] p; // The planes of the frustum
	
	public bool inFrustum;
	
	void Start() {
		eyes = gameObject.GetComponent<Camera>();
		inFrustum = false;
	}
	
	void Update() {
		p = GeometryUtility.CalculateFrustumPlanes(eyes);
		Bounds bounds = target.GetComponent<Collider>().bounds;
		if ( GeometryUtility.TestPlanesAABB(p, bounds) ) {
			Debug.Log(target.name + " has been detected!");
			inFrustum = true;
		} else {
			Debug.Log("Nothing has been detected");
			inFrustum = false;
		}
	}
}
