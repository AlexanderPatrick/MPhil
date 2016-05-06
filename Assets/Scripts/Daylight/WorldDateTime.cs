using UnityEngine;
using System;
using System.Collections;

namespace NPC {
	public class WorldDateTime : MonoBehaviour {
		private static WorldDateTime instance;
		
		public static WorldDateTime getInstance() {
			if (!instance) {
				instance = FindObjectOfType<WorldDateTime>();
				if (!instance) {
					Debug.LogError("The concept of time does not exist in this world. Yet something is trying to perceive it.");
				}
			}
			return instance;
		}
		
		private WorldDateTime() {
		}
	
		public int startYear = 1, startMonth = 1, startDay = 1, startHour = 0, startMinute = 0, startSecond = 0;
		public float worldTimeScale;
		public float deltaTime;
        public bool debug;
		
		private DateTime dateTime;
		
		public int Year {
			get { return dateTime.Year; }
		}
		
		public int Month {
			get { return dateTime.Month; }
		}
		
		public int Day {
			get { return dateTime.Day; }
		}
		
		public int Hour {
			get { return dateTime.Hour; }
		}
		
		public int Minute {
			get { return dateTime.Minute; }
		}
		
		public int Second {
			get { return dateTime.Second; }
		}
		
		// Use this for initialization
		void Start () {
			dateTime = new DateTime(startYear, startMonth, startDay, startHour, startMinute, startSecond);
		}
		
		// Update is called once per frame
		void Update () {
			DateTime newTime = dateTime.AddSeconds(Time.deltaTime * worldTimeScale);
            if (debug) {
                Debug.Log(newTime);
            }
			TimeSpan timeSpan = newTime.Subtract(dateTime);
			dateTime = newTime;
			deltaTime = (float) timeSpan.TotalSeconds;
		}
	}
}