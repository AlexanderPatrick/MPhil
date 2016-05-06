using UnityEngine;

public class Turn : MonoBehaviour {
    public float speed;

    private new Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        //rigidbody.angularDrag = Mathf.Infinity;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rigidbody.angularVelocity = transform.up * speed;
	}
}
