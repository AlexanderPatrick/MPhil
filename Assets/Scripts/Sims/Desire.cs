using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Desire : MonoBehaviour {
    private Dictionary<DesireType, float> geneticBaseStrength;
    private Dictionary<DesireType, float> culturalStrengthModifer;

	// Use this for initialization
	void Start () {
        geneticBaseStrength = new Dictionary<DesireType, float>();
        culturalStrengthModifer = new Dictionary<DesireType, float>();
	    // Randomise the genes
        foreach (DesireType desireType in System.Enum.GetValues(typeof(DesireType))) {
            geneticBaseStrength[desireType] = Random.value;
            culturalStrengthModifer[desireType] = 1;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            string message = "";
            foreach (DesireType desireType in System.Enum.GetValues(typeof(DesireType))) {
                message = message + System.Enum.GetName(typeof(DesireType), desireType) + ": " + geneticBaseStrength[desireType] * culturalStrengthModifer[desireType] + "\n";
            }
            Debug.Log(message);
        }
	}
}

public enum DesireType {
    Power,              // The desire to influence others
    Independence,       // The desire for self-reliance
    Curiosity,          // The desire for knowledge
    Acceptance,         // The desire for inclusion
    Order,              // The desire for organisation
    Saving,             // The desire to collect things
    Honour,             // The desire to be loyal to one's parents and heritage
    Idealism,           // The desire for social justice
    SocialContact,      // The desire for companionship
    Family,             // The desire to raise one's own children
    Status,             // The desire for social standing
    Vengeance,          // The desire to get even
    Romance,            // The desire for sex and beauty
    Eating,             // The desire to consume food
    PhysicalActivity,   // The desire for exercise of muscles
    Tranquility         // The desire for emotional calm
}

/*
    Saving,         // The desire to hoard and collect
    Construction,   // The desire to build and achieve
    Curiosity,      // The desire to explore and learn
    Exhibition,     // The desire for attention
    Family,         // The desire to raise our children
    Hunting,        // The desire to find food
    Order,          // The desire for cleanliness and organisation
    Play,           // The desire for fun
    Sex,            // The desire to reproduce
    Shame,          // The desire to avoid being singled out
    Pain,           // The desire to avoid aversive sensation
    Herd,           // The desire for social contact
    Vengeance       // The desire for aggression
*/