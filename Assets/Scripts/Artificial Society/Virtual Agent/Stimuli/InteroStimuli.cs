using UnityEngine;
using System.Collections;

public class InteroStimuli : MonoBehaviour {
    public float hunger;
    public float thirst;
	// Use this for initialization
	void Start () {
        hunger = 0;
        thirst = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Feed(float amount) {
        hunger = Mathf.Clamp01(hunger - amount);
    }
    public void Quench(float amount) {
        thirst = Mathf.Clamp01(thirst - amount);
    }
}
