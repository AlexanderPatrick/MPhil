using UnityEngine;
using System.Collections.Generic;
using AlexanderPatrick;

public class Gravity : PersistentSingletonMonoBehaviour<Gravity> {
    private List<GravityGenerator> gravityGenerators;
    private List<Rigidbody> gravityAffected;

	// Use this for initialization
	void Start () {
	   // masses = new List<Mass>(GameObject.FindObjectsOfType<Mass>());
	}
	
	// Update is called once per frame
	void Update () {
        /*foreach (Mass mass in masses) {
            Rigidbody massRigidbody = mass.GetComponent<Rigidbody>();
            if (massRigidbody != null) {
                float distance = Vector3.Distance(mass.transform.position, transform.position);
                massRigidbody.position = Vector3.MoveTowards(massRigidbody.position, transform.position, distance * Time.deltaTime);
            }
        }*/
	}

    private void calculateStrongestGravityField() {

    }
}
