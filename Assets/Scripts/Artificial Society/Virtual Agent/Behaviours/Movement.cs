using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    public new Rigidbody rigidbody;
    public float force;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKey(KeyCode.UpArrow)) {
            rigidbody.AddForce(rigidbody.transform.forward * force);
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            rigidbody.AddForce(rigidbody.transform.right * force);
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            rigidbody.AddForce(-rigidbody.transform.forward * force);
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            rigidbody.AddForce(-rigidbody.transform.right * force);
        }
	}
}
