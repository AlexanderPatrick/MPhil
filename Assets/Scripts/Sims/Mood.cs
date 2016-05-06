using UnityEngine;
using System.Collections;

public class Mood : MonoBehaviour {

    public float mood = 0;
    public Range hungerRange;
    public Range activeHungerRange;
    public float hunger;

    [System.Serializable]
    public class Range {
        public float minimum;
        public float maximum;
        public Range(float min, float max) {
            minimum = min;
            maximum = max;
        }
    }

	// Use this for initialization
	void Start () {
        hungerRange = new Range(-100, 100);
        activeHungerRange = new Range(-40, 40);
	}
	
	// Update is called once per frame
	void Update () {
        hunger -= Time.deltaTime;
        if (hunger < activeHungerRange.minimum) {
            mood = -10;
            StartCoroutine(GetFood());
        }
	}

    IEnumerator GetFood() {

        yield return null;
    }
}
