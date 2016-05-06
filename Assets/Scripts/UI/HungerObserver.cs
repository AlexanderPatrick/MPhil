using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HungerObserver : MonoBehaviour {
    public Hunger hungerReference;
    private Text text;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Hunger: " + hungerReference.HungerValue + "/" + hungerReference.hungerLimit;
	}
}
