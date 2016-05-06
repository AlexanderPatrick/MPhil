using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Memories {
    [System.Serializable]
    public class ProceduralMemory : IEnumerable{
        [SerializeField]
        private List<Skill> skills;

        public ProceduralMemory() {
            skills = new List<Skill>();
        }

        public int Count {
            get { return skills.Count; }
        }

        public List<Skill> AvailableSkills {
            get { return skills.Where(s => s.available == true).ToList(); }
        }

        public void AddSkill(Skill skill) {
            skills.Add(skill);
        }

        public Skill FindSkill(string name) {
            Skill skillToReturn = null;
            foreach (Skill skill in skills) {
                if (skill.name == name) {
                    skillToReturn = skill;
                    break;
                }
            }
            return skillToReturn;
        }

        public void RemoveSkill(string name) {
            skills.Remove(FindSkill(name)); // Curious how this handles removing skills it can't find

            //for (int i = skills.Count - 1; i >= 0; i-- ) {
            //    if (skills[i].name == name) {
            //        skills.RemoveAt(i);
            //    }
            //}
        }

        public int SkillNameToIndex(string name) {
            for (int i = 0; i < Count; i++) {
                if (skills[i].name == name) {
                    return i;
                }
            }
            Debug.LogError(name + " is not a known skill.");
            return -1;
        }
        
        public Skill this[int i] {
            get { return skills[i]; }
            set { skills[i] = value; }
        }

        public Skill this[string name] {
            get { return FindSkill(name); }
            set {
                for (int i = skills.Count - 1; i >= 0; i--) {
                    if (skills[i].name == name) {
                        skills[i] = value;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return skills.GetEnumerator();
        }
    }
}