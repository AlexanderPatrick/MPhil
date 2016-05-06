using System;
using UnityEngine;

[Serializable]
public class TimePeriod {

	public string timeString;
	DateTime startTime;
	DateTime endTime;
	
	public TimePeriod() {
		timeString = "";
		startTime = DateTime.Now; // Should be Gameworld's Time;
	}
	
	void buildTimeString() {
		if (startTime < DateTime.Now) {
			timeString = "before";
		} else if (startTime == DateTime.Now) {
			timeString = "now";
		} else if (startTime > DateTime.Now) {
			timeString = "later";
		}
	}
}
