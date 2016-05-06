using UnityEngine;
using System.Collections;

public class Hunger : AbstractSense {
    public float metabolism = 1; // hunger increases by metabolism each second
    public float hungerLimit = 1814400; // 21 * 24 * 60 * 60; // 21 days; Mahatma Gandhi

    private float hunger = 0;
    public float HungerValue {
        get { return hunger; }
        private set { }
    }

	// Use this for initialization
	void Start () {
        hunger = 0;
	}
	
	// Update is called once per frame
	void Update () {
        hunger += metabolism * Time.deltaTime;
        if (hunger > hungerLimit) {
            Debug.LogError("System failure: Entity died of hunger");
        }
	}

    public float Sense() {
        return hunger;
    }

    public void Feed(float food) {
        hunger -= food;
        hunger = Mathf.Max(hunger, 0);
    }
}
