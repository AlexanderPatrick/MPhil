using UnityEngine;
using System.Collections;

public class Look : MonoBehaviour {
    public Transform eye;

    private float distance;
    private RaycastHit hitInfo;
    private Ray lineOfSight;
        

    public float Distance {
        get { return distance; }
    }

	// Use this for initialization
	void Start () {
        if (eye == null) {
            Debug.LogWarning("No eyes, can't see");
            this.enabled = false;
            return;
        }
	}
	
	// Update is called once per frame
	void Update () {
        lineOfSight = new Ray(eye.position, eye.forward);
        //if (Physics.Raycast(lineOfSight, out hitInfo)) {
        //    distance = hitInfo.distance;
        //    Debug.Log(distance);
        //}
        if (Physics.SphereCast(lineOfSight, 1, out hitInfo)) {
            distance = hitInfo.distance;
            Debug.Log(distance + " - " + hitInfo.collider.name);
        }
	}
}
