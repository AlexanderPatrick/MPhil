using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace NPC {
    public class Sight : MonoBehaviour {
        private List<Texture2D> iconicMemory;
        private Texture2D texture2D;

        // Use this for initialization
        void Start() {
            // iconicMemory = GetComponent<Memory>().sensoryMemory.iconicMemory;
            // texture2D = GetComponent<CamToTex>().tex;
        }

        // Update is called once per frame
        void Update() {
            // iconicMemory.Add(texture2D);
            // if (iconicMemory.Count > 4) {
            //	iconicMemory.RemoveAt(0);
            // }
        }
    }

}
