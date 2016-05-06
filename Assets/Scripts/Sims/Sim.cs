using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Sim : MonoBehaviour {
    public SimMotives motives;
    SimMotives oldMotives;

	// Use this for initialization
	void Start () {
        motives.LifetimeHappiness = 0;
        motives.WeeklyHappiness = 0;
        motives.DailyHappiness = 0;
        motives.Happiness = 0;
        motives.Physical = 0;
        motives.Energy = 70;
        motives.Comfort = 0;
        motives.Hunger = -40;
        motives.Hygiene = 0;
        motives.Bladder = 0;
        motives.Mental = 0;
        motives.Alertness = 20;
        motives.Stress = 0;
        motives.Environment = 0;
        motives.Social = 0;
        motives.Entertained = 0;
	}
	
	// Update is called once per frame
	void Update () {
        // energy
        if (motives.Energy > 0) {
            if (motives.Alertness > 0) {
                // linear decrease
                motives.Energy -= motives.Alertness / 100 * Time.deltaTime;
            } else {
                // flattening curve increase
                motives.Energy -= motives.Alertness / 100 * (100 - motives.Energy) / 50 * Time.deltaTime;
            }
        } else {
            if (motives.Alertness > 0) {
                motives.Energy -= motives.Alertness / 100 * (100 - motives.Energy) / 50 * Time.deltaTime;
            } else {
                motives.Energy -= motives.Alertness / 100 * Time.deltaTime;
            }
        }

        // comfort
        if (motives.Bladder < 0) {
            // decrease by a maximum of 10
            motives.Comfort += motives.Bladder / 10 * Time.deltaTime; 
        }
        if (motives.Hygiene < 0) {
            // decrease by a maximum of 5
            motives.Comfort += motives.Hygiene / 20 * Time.deltaTime;
        }
        if (motives.Hunger < 0) {
            // decrease by a maximum of 5
            motives.Comfort += motives.Hunger / 20 * Time.deltaTime;
        }
        motives.Comfort -= motives.Comfort * motives.Comfort * motives.Comfort / 10000 * Time.deltaTime;

        // hunger
        motives.Hunger -= (motives.Alertness + 100) / 200 * (motives.Hunger + 100) / 100 * Time.deltaTime;
        if (motives.Stress < 0) {
            motives.Hunger += motives.Stress / 100 * (motives.Hunger + 100) / 100 * Time.deltaTime;
        }
        if (motives.Hunger < -99) {
            Debug.Log("You have starved to death.");
            motives.Hunger = 80;
        }

        // hygiene
        if (motives.Alertness > 0) {
            motives.Hygiene -= 0.3f * Time.deltaTime;
        } else {
            motives.Hygiene -= 0.1f * Time.deltaTime;
        }

        if (motives.Hygiene < -97) {
            // hit limit, bathe
            Debug.Log("You smell very bad, mandatory bath");
            motives.Hygiene = 80;
        }

        // bladder
        if (motives.Alertness > 0) {
            // bladder fills faster while awake
            motives.Bladder -= 0.4f * Time.deltaTime;
        } else {
            motives.Bladder -= 0.2f * Time.deltaTime;
        }
        if (motives.Hunger > oldMotives.Hunger) {
            // food eaten goes into bladder
            motives.Bladder -= (motives.Hunger - oldMotives.Hunger) / 4 * Time.deltaTime;
        }
        if (motives.Bladder < -97) {
            // hit limit, gotta go
            if (motives.Alertness < 0) {
                Debug.Log("You have wet the bed");
            } else {
                Debug.Log("You have soiled the carpet");
            }
            motives.Bladder = 90;
        }

        // alertness
        float temp;
        if (motives.Alertness > 0) {
            temp = (100 - motives.Alertness) / 50;
        } else {
            temp = (motives.Alertness + 100) / 50;
        }

        if (motives.Energy > 0) {
            if (motives.Alertness > 0) {
                motives.Alertness += motives.Energy / 100 * temp * Time.deltaTime;
            } else {
                motives.Alertness += motives.Energy / 100 * Time.deltaTime;
            }
        } else {
            if (motives.Alertness > 0) {
                motives.Alertness += motives.Energy / 100 * Time.deltaTime;
            } else {
                motives.Alertness += motives.Energy / 100 * temp * Time.deltaTime;
            }
        }
        motives.Alertness += motives.Entertained / 300 * temp * Time.deltaTime;
        if (motives.Bladder < -50) {
            motives.Alertness -= motives.Bladder / 100 * temp * Time.deltaTime;
        }

        // stress
        motives.Stress += motives.Comfort / 10 * Time.deltaTime;
        motives.Stress += motives.Entertained / 10 * Time.deltaTime;
        motives.Stress += motives.Environment / 15 * Time.deltaTime;
        motives.Stress += motives.Social / 20 * Time.deltaTime;
        if (motives.Alertness < 0) {
            // cut stress while asleep
            motives.Stress /= 3 * Time.deltaTime;
        }
        motives.Stress -= motives.Stress * motives.Stress * motives.Stress / 10000 * Time.deltaTime;
        if (motives.Stress < 0) {
            if (UnityEngine.Random.Range(-100, -70) > motives.Stress) {
                if (UnityEngine.Random.Range(-100, -70) > motives.Stress) {
                    Debug.Log("You have lost your temper");
                    motives.Stress += 20;
                }
            }
        }

        // environment

        // social

        // entertained
        if (motives.Alertness < 0) {
            // cut entertained while asleep
            motives.Entertained /= 2;
        }

        // physical
        temp = (motives.Energy + motives.Comfort + motives.Hunger + motives.Hygiene + motives.Bladder) /5;
        if (temp > 0) {
            // map the linear average into squared curve
            temp = 100 - temp;
            temp = temp * temp / 100;
            temp -= 100;
        } else {
            temp = 100 + temp;
            temp = temp * temp / 100;
            temp -= 100;
        }
        motives.Physical = temp;

        // mental
        temp = (motives.Stress * 2 + motives.Environment + motives.Social + motives.Entertained) / 5;
        if (temp > 0) {
            // map the linear average into squared curve?
            temp = 100 - temp;
            temp = temp * temp / 100;
            temp -= 100;
        } else {
            temp = 100 + temp;
            temp = temp * temp / 100;
            temp -= 100;
        }
        motives.Mental = temp;

        // happiness
        motives.Happiness = (motives.Physical + motives.Mental) / 2;
        if (Time.time > 0) {
            motives.LifetimeHappiness = (motives.LifetimeHappiness * (Time.time - Time.deltaTime) + motives.Happiness * Time.deltaTime) / Time.time;
        }

        oldMotives.LifetimeHappiness = motives.LifetimeHappiness;
        oldMotives.LifetimeHappiness = motives.LifetimeHappiness;
        oldMotives.WeeklyHappiness = motives.WeeklyHappiness;
        oldMotives.DailyHappiness = motives.DailyHappiness;
        oldMotives.Happiness = motives.Happiness;
        oldMotives.Physical = motives.Physical;
        oldMotives.Energy = motives.Energy;
        oldMotives.Comfort = motives.Comfort;
        oldMotives.Hunger = motives.Hunger;
        oldMotives.Hygiene = motives.Hygiene;
        oldMotives.Bladder = motives.Bladder;
        oldMotives.Mental = motives.Mental;
        oldMotives.Alertness = motives.Alertness;
        oldMotives.Stress = motives.Stress;
        oldMotives.Environment = motives.Environment;
        oldMotives.Social = motives.Social;
        oldMotives.Entertained = motives.Entertained;
	}
}

[Serializable]
public struct SimMotives {
    public float LifetimeHappiness;
    public float WeeklyHappiness;
    public float DailyHappiness;
    public float Happiness;
    public float Physical;
    public float Energy;
    public float Comfort;
    public float Hunger;
    public float Hygiene;
    public float Bladder;
    public float Mental;
    public float Alertness;
    public float Stress;
    public float Environment;
    public float Social;
    public float Entertained;
}