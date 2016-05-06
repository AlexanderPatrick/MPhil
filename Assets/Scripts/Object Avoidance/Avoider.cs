using UnityEngine;

public class Avoider : MonoBehaviour {
    public float comfortZone;

    private Walk walk;
    private Turn turn;
    private Look look;

	// Use this for initialization
	void Start () {
        walk = GetComponent<Walk>();
        turn = GetComponent<Turn>();
        look = GetComponent<Look>();
        // walk.speed = 1;
        // turn.speed = 0;
	}
    
    void OnCollisionEnter(Collision collision) {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name != "Plane") {
            Debug.Log("Ow");
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (look.Distance < comfortZone * walk.speed) {
            turn.speed = 10;
        } else {
            turn.speed = 0;
        }
	}
}
