using UnityEngine;
using System.Collections;

public class RigidMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        GetComponent<Rigidbody>().AddForce(h, v, 0, ForceMode.Force);
	}
}