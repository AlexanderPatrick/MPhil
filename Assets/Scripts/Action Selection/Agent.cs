using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ActionSelection {
    public class Agent : MonoBehaviour {
        [Header("Tweakables")]
        public ActionSelectionMode actionSelectionMode;
        public RewardCumulation rewardCumulationMode;
        public int memoryLimit;
        public int threshold;
        public int boredomUpperLimit;
        public int boredomLowerLimit;
        public float noveltyFactor = 1;
        public Agent target;

        [Header("Watchables")]
        public int happiness;
        [SerializeField]
        int previousHappiness;
        [SerializeField]
        int happinessDelta;
        [SerializeField]
        Action selectedAction;
        [SerializeField]
        List<Action> actions;
        [SerializeField]
        List<Reward> rewards;

        new Renderer renderer;
        

        // Use this for initialization
        void Start() {
            renderer = GetComponent<Renderer>();

            happiness = 0;
            previousHappiness = happiness;

            actions = new List<Action>();
            actions.Add(new Action("Do Nothing", () => { BecomeWhite(); }));

            actions.Add(new Action("Increase Happiness", () => { happiness++; BecomeWhite(); }));
            actions.Add(new Action("Decrease Happiness", () => { happiness--; BecomeWhite(); }));

            actions.Add(new Action("Mudita"       , () => { if(target.happiness > happiness) happiness+=3; BecomeYellow(); }));
            actions.Add(new Action("Envy"         , () => { if(target.happiness > happiness) happiness-=2; BecomeGreen(); }));
            actions.Add(new Action("Schadenfreude", () => { if(target.happiness < happiness) happiness+=2; BecomeRed(); }));
            actions.Add(new Action("Pity"         , () => { if(target.happiness < happiness) happiness-=3; BecomeBlue(); }));
            selectedAction = new Action("Undefined", null);

            rewards = new List<Reward>();

            string debugString = gameObject.name + " Available Actions: " + actions.Count + " = ";
            foreach (Action action in actions) {
                debugString += action + ", ";
            }
            Debug.Log(debugString);
        }

        // Update is called once per frame
        void Update() {
            List<Action> availableActions = actions.FindAll(a => a.allowed = true);
            // Action Selection
            #region RandomActionSelection
            if (actionSelectionMode == ActionSelectionMode.Random) {
                int actionIndex = UnityEngine.Random.Range(0, availableActions.Count);
                selectedAction = availableActions[actionIndex];
            }
            #endregion
            #region BestActionSelection
            if (actionSelectionMode == ActionSelectionMode.Best) {
                int max = availableActions.Max((Action a) => { return a.reward; });
                List<Action> bestActions = availableActions.FindAll(a => a.reward >= max - threshold);
                int bestActionIndex = UnityEngine.Random.Range(0, bestActions.Count);
                selectedAction = bestActions[bestActionIndex];
            }
            #endregion

            // Perform Action
            selectedAction.action();
            selectedAction.boredom++;
        }

        void LateUpdate() {
            // Update reward variable;
            happinessDelta = happiness - previousHappiness;
            previousHappiness = happiness;

            // Store experience
            rewards.Add(new Reward(selectedAction, happinessDelta));
            for (int i = 0; i < actions.Count; i++) {
                // Limit memories to the most recent 
                List<Reward> workingMemory = rewards.Skip(rewards.Count - memoryLimit).ToList();

                IEnumerable<Reward> actionRewards = workingMemory.Where((Reward r) => { return r.action == actions[i]; });
                Action action = actions[i];

                // Sum
                if (rewardCumulationMode == RewardCumulation.Sum) {
                    action.reward = actionRewards.Sum((Reward r) => { return r.reward; });
                }

                // Average
                if (rewardCumulationMode == RewardCumulation.Average) {
                    if (actionRewards.Count() > 0) {
                        action.reward = (int)actionRewards.Sum((Reward r) => { return r.reward; }) / actionRewards.Count();
                    } else {
                        action.reward = 0;
                    }
                }

                // Sum Decay
                if (rewardCumulationMode == RewardCumulation.SumDecay) {
                    if (workingMemory.Count > 0) {
                        int decayedSum = 0;
                        for (int j = 0; j < workingMemory.Count; j++) {
                            if (action == workingMemory[j].action) {
                                decayedSum += workingMemory[j].reward / (workingMemory.Count - j);
                            }
                        }
                        action.reward = decayedSum;
                    }
                }

                // Boredom calculation
                if (memoryLimit > 0) {
                    float boredomSum = 0;
                    for (int j = 0; j < workingMemory.Count; j++) {
                        if (action.name == workingMemory[j].action.name) {
                            boredomSum += Mathf.Lerp(boredomLowerLimit, boredomUpperLimit, j / memoryLimit);
                        }
                    }
                    action.boredom = (int)boredomSum;
                }

                // Novelty calculation
                Reward rewardLastTimeActionPerformed = workingMemory.Where((Reward r) => { return r.action.name == action.name; }).Last();
                if (rewardLastTimeActionPerformed.action == action) {
                    int rewardIndex = workingMemory.LastIndexOf(rewardLastTimeActionPerformed);
                    int difference = workingMemory.Count - rewardIndex;
                    float novelty = -Mathf.Exp(-difference * noveltyFactor) + 1;
                    
                }

                actions[i] = action;
            }

            string debugString = gameObject.name + " Action Performed: " + selectedAction + ", Reward: " + happinessDelta + ", Happiness: " + happiness + "\n";
            foreach (Action action in actions) {
                debugString += action + "=" + action.reward + ", ";
            }
            Debug.Log(debugString);
        }

        private void BecomeWhite() {
            renderer.material.color = new Color(1, 1, 1);
        }

        private void BecomeYellow() {
            renderer.material.color = new Color(1, 1, 0);
        }

        private void BecomeGreen() {
            renderer.material.color = new Color(0, 1, 0);
        }

        private void BecomeRed() {
            renderer.material.color = new Color(1, 0, 0);
        }

        private void BecomeBlue() {
            renderer.material.color = new Color(0, 0, 1);
        }
    }

    [Serializable]
    public struct Action {
        public string name;
        public System.Action action;
        public bool allowed;
        public int reward;
        public int boredom;
        public float novelty;

        public Action(string name = "", System.Action action = null, bool allowed = true, int reward = 0, int boredom = 0, float novelty = 0) {
            this.name = name;
            this.action = action;
            this.allowed = allowed;
            this.reward = reward;
            this.boredom = boredom;
            this.novelty = novelty;
        }

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public static bool operator ==(Action lhs, Action rhs) {
            return lhs.name == rhs.name;
        }

        public static bool operator !=(Action lhs, Action rhs) {
            return !(lhs == rhs);
        }

        public override string ToString() {
            return this.name;
        }
    }

    [Serializable]
    public struct Reward {
        public Action action;
        public int reward;

        public Reward(Action action, int reward) {
            this.action = action;
            this.reward = reward;
        }
    }

    public enum ActionSelectionMode {
        Random,
        Best
    }

    public enum RewardCumulation {
        Sum,
        Average,
        SumDecay
    }
}