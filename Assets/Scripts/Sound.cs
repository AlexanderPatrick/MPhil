using UnityEngine;
using Senses;
using System.Collections;

public class Sound : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<AudioSource>().isPlaying) {
			Collider[] objectsWithinHearingRange = Physics.OverlapSphere(transform.position, GetComponent<AudioSource>().maxDistance);
			foreach (Collider item in objectsWithinHearingRange) {
				if (item.GetComponent<Audition>()) {
					item.SendMessage("HearSound", GetComponent<AudioSource>().clip);
				}
			}
		}
	}
}
