using UnityEngine;
using System.Collections;

public class Walk : MonoBehaviour {
    public float speed;

    private new Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        //rigidbody.drag = Mathf.Infinity;
	}

    void FixedUpdate() {
        rigidbody.velocity = transform.forward * speed;
    }
}
