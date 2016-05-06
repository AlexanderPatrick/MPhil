using UnityEngine;
using System.Collections;

public class Zone : MonoBehaviour {
    public Bounds bounds;

    public float volume {
        get { return bounds.extents.x * bounds.extents.y * bounds.extents.z; }
    }

	// Use this for initialization
	void Start () {
        (GetComponent<Collider>() as BoxCollider).center = bounds.center;
        (GetComponent<Collider>() as BoxCollider).size = bounds.extents;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        // Debug.Log(name + " OnTriggerEnter:" + other.name);
        Zone otherZone = other.GetComponent<Zone>();
        if (otherZone) {
            if (volume > otherZone.volume) {
                other.transform.parent = transform;
            }
        } else {
            Transform otherParent = other.transform.parent;
            if (otherParent) {
                otherZone = otherParent.GetComponent<Zone>();
                if (volume < otherZone.volume) {
                    other.transform.parent = transform;
                }
            } else {
                other.transform.parent = transform;
            }
        }
    }
}
