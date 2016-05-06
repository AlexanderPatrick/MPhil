using UnityEngine;
using System;

namespace Memories {
    [Serializable]
    public class Skill {
        public string name;
        public Action method;
        public bool available;
        public float novelty;
        public float boredom;

        public Skill(string name, Action method, bool available = true, float novelty = 1, float boredom = 0) {
            this.name = name;
            this.method = method;
            this.available = available;
            this.novelty = novelty;
            this.boredom = boredom;
        }
    }
}