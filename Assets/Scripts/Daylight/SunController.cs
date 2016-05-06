using UnityEngine;
using System.Collections;

namespace NPC {
	public class SunController : MonoBehaviour {
		private WorldDateTime worldDateTime;
		// Use this for initialization
		void Start () {
			worldDateTime = WorldDateTime.getInstance();
		}
		
		// Update is called once per frame
		void Update () {
			int seconds = worldDateTime.Hour * 3600 + worldDateTime.Minute * 60 + worldDateTime.Second;
			float angle = (float)seconds/86400 * 360;
			transform.rotation = Quaternion.Euler( new Vector3(90, angle, 0) );
		}
	}
}