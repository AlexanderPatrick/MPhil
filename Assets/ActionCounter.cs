using UnityEngine;
using UnityEngine.UI;

public class ActionCounter : MonoBehaviour {
    public int nothing = 0;
    public int lighton = 0;
    public int lightoff = 0;
    public int pressbutton = 0;

    private Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Do Nothing: " + nothing + "\nLight On: " + lighton + "\nLight Off: " + lightoff + "\nPress Button: " + pressbutton;
	}
}
