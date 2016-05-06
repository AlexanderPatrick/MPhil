using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NPC {
	public class Memory : MonoBehaviour {
		public SensoryMemory sensoryMemory;
		public WorkingMemory workingMemory;
		public LongTermMemory longTermMemory;
		
		// Use this for initialization
		void Start () {
			sensoryMemory = new SensoryMemory();
			workingMemory = new WorkingMemory();
			longTermMemory = new LongTermMemory();

			// Preprogramming of basic functionality
			longTermMemory.proceduralMemory.Add("Focus On", FocusOn);
			longTermMemory.proceduralMemory.Add("Move To", MoveTo);
			
			// Preprogramming of goal (for now)
			longTermMemory.prospectiveMemory.Add( new Prospect("Focus On", "Cube", "Now") );
			
			// Prefilling of Visible object (until a suitable alternative to "see")
			sensoryMemory.iconicMemory.Add( GameObject.Find("Cube") );
		}
		
		// Update is called once per frame
		void Update () {
			// We find some goal. I believe that we always have some goal, some intention at all times.
			if (workingMemory.intent == null) {
				workingMemory.intent = longTermMemory.prospectiveMemory[0];
			}
			
			if ( relevant(workingMemory.intent.cue) ) {
				if ( longTermMemory.proceduralMemory.ContainsKey(workingMemory.intent.action) ) {
					if (sensoryMemory.iconicMemory[0].name == "Cube") {
						longTermMemory.proceduralMemory[workingMemory.intent.action](sensoryMemory.iconicMemory[0]);
					}
				} else {
					// recursive planning down to base tasks if not inside procedural memory
					// planning(workingMemory.intent.action);
				}
			}
			
			
			// runCognitiveProcesses();
			// 	attention();		
			//	intuition();		// instinctive knowing (without the use of rational processes)
			//	perception();		// the process of perceiving
			//	apperception(); 	// the process whereby perceived qualities of an object are related to past experience
			//	believing();		// the cognitive process that leads to convictions; "seeing is believing"
			//	classification();	// the basic cognitive process of arranging into classes or categories
			//	discernment();		// the cognitive process whereby two or more stimuli are distinguished
			//	learning();			// the cognitive process of acquiring skill or knowledge; "the child's acquisition of language"
			//	remembering();		// the cognitive processes whereby past experience is remembered; "he can do it from memory"; "he enjoyed remembering his father"
			//	representationalProcess();	// any basic cognitive process in which some entity comes to stand for or represent something else
		}
		
		#region Cognitive Processes
		
		void attention() {
			Debug.Log ("Attention");
			// the process whereby a person concentrates on some features of the environment to the (relative) exclusion of others
			// takes input from the current plan/goal on what to focus on 
			// output is that it elaborates features of the focus target allowing the perception process to work better on it.
			// additionally it reduces information available outside of the focus area. 
		}
		
		
		void intuition() {
			// A bit tricky to describe due to its unconscious nature.
			// It may require subconscious recall of episodes using cues in sensory memory that may or may not have received focus 
			// input being subconscious cues from sensory memory 
			// which then trigger output micro-emotion from subconciously recalled episodes.
		}
		
		/// <summary>
		/// The transfer from sensory memory representation to working memory representation. 
		/// </summary>
		void perception() {
		}
		
		void apperception() {
			// e.g. A rich child and a poor child walking together come across the same ten dollar bill on the sidewalk. 
			// The rich child says it is not very much money and the poor child says it is a lot of money. 
			// The difference lies in how they apperceive the same event – the lens of past experience through which they see and value (or devalue) the money.
		}
		
		/// <summary>
		/// Abstracting patterns in episodic memory into semantic memory?
		/// </summary>
		void believing() { 
		}
		
		/// <summary>
		/// Establishing Is-A relationships between different concepts in semantic memory.
		/// From supervised learning, taking features as inputs, processing them with the learning algorithm and learnt data and outputting a discrete value. 
		/// </summary>
		void classification() { // I guess discrete value could be bool more like an enum
		}
		
		/// <summary>
		/// From supervised learning, taking features as inputs, processing them with the learning algorithm and learnt data and outputting a continuous value. 
		/// </summary>
		float regression() {
			return 0;
		}
		
		/// <summary>
		/// Identifying differences between two similar concepts in semantic/episodic memory
		/// </summary>
		void discernment() { 
		}
		
		/// <summary>
		/// Reinforcing connections within long-term memory.
		/// </summary>
		void learning() {
		}
		
		/// <summary>
		/// Takes a cue as input and outputs an episode or episodes related to that cue from episodic memory.
		/// </summary>
		void /* Episode(s) */ remembering(/* Cue */) {
		}
		
		/// <summary>
		/// Takes as input a goal and returns its reasoning for wanting to achieve that goal
		/// </summary>
		void /* Goal */ rationalising(/* Goal */) {
		}
		
		/// <summary>
		/// Takes as input a goal and returns steps(subgoals/actions) required to satisfy that goal.
		/// </summary>
		void /* Goal */ planning(/* Goal */) {
		}
		
		#endregion
		List<Episode> WhatHappenedAtTime(DateTime dateTime) {
			return null;
		}
		
		DateTime WhenDidEpisodeHappen(Episode episode) {
			return DateTime.MinValue;
		}
		
		/// <summary>
		/// Checks whether the supplied cue is relevant to the current context
		/// Used to determine if a prospect's cue is currently relevant
		/// </summary>
		/// <param name="cue">The cue to determine if relevant to the current situation/context</param>
		bool relevant(/* Cue */ string cue) {
			if (cue == "Now") {
				return true;
			}
			return false;
		}
		
		#region Base Skills
		/// <summary>
		/// A base skill that allows the agent to focus on a specified target.
		/// </summary>
		/// <param name="target">The target to focus on.</param>
		void FocusOn(GameObject target) {
			transform.LookAt(target.transform.position);
		}
		
		void MoveTo(GameObject target) {
			//transform.Translate( (target.transform.position - transform.position).normalized );
			GetComponent<CharacterController>().Move( new Vector3(0,0,1*Time.deltaTime) );
		}
		#endregion
		
		[Serializable]
		public class SensoryMemory {
			public List<GameObject> iconicMemory;
			public List<GameObject> echoicMemory;
			public List<GameObject> hapticMemory;
			public List<GameObject> gusticMemory;
			public List<GameObject> olfacticMemory;
			
			public SensoryMemory() {
				iconicMemory = new List<GameObject>();
				echoicMemory = new List<GameObject>();
				hapticMemory = new List<GameObject>();
				gusticMemory = new List<GameObject>();
				olfacticMemory = new List<GameObject>();
			}
		}
		
		[Serializable]
		public class WorkingMemory {
			public List<GameObject> visuospatialSketchpad;
			public PhonologicalLoop phonologicalLoop;
			public Episode episodicBuffer;
			public Prospect intent;
			
			public WorkingMemory() {
				visuospatialSketchpad = new List<GameObject>();
				phonologicalLoop = new PhonologicalLoop();
				episodicBuffer = new Episode();
				intent = null;
			}
		}
		
		[Serializable]
		public class PhonologicalLoop {
			public List<AudioSource> acousticStore;
			public string articulatoryControlSystem;
			
			public PhonologicalLoop() {
				acousticStore = new List<AudioSource>();
				articulatoryControlSystem = "";
			}
		}
		
		[Serializable]
		public class LongTermMemory {
			public List<SemanticRelationship> semanticMemory; // Knowledge Memory
			public List<Episode> autobiographicalMemory; // History Memory
			public Dictionary< String,Action<GameObject> > proceduralMemory; // Skills Memory
			public List<Prospect> prospectiveMemory; // Goal Memory
			
			public LongTermMemory() {
				semanticMemory = new List<SemanticRelationship>();
				autobiographicalMemory = new List<Episode>();
				proceduralMemory = new Dictionary< string, Action<GameObject> >();
				prospectiveMemory = new List<Prospect>();
			}
		}
		
		[Serializable]
		public class SemanticRelationship {
			public GameObject subject;	// Paris				|	a capital
			public string relationship;	// is the capital of	|	is the main city of
			public GameObject target;	// France				|	a country
		}
		
		[Serializable]
		public class Episode {
			public Episode context;	// "While watching Days of our Lives";
			public string who;		// "The scriptwriters";
			public string what;		// "shocked";
			public string whom;		// "Bo";
			public string how;		// "with a Diabolus Ex Machina";
			public Location where;	// "in Bayou Degare";
			public TimePeriod when;	// "at night";
			public Motive why;		// "to prolong the story";
			// emotionalImpact		// "It was funny";
			
			public Episode() {
				
			}
		}
		
		[Serializable]
		public class Prospect {
			public string action;	// Buy
			public string target;	// eggs
			public string cue;		// on my way home
			
			public Prospect(string action, string target = "", string cue = "Now") {
				this.action = action;
				this.target = target;
				this.cue = cue;
			}
		}
	}
}