using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Memories;
using Stimuli;

namespace NPC {
    public class Vision : MonoBehaviour {
        public Memory memory;

        List<GameObject> objectsWithinViewArea;

        // Use this for initialization
        void Start() {
            if (!memory) {
                this.enabled = false;
            }

            objectsWithinViewArea = new List<GameObject>();
        }

        // Update is called once per frame
        void Update() {
            foreach (GameObject item in objectsWithinViewArea) {
                VisualStimulus visualStimulus = item.GetComponent<VisualStimulus>();
                if (visualStimulus != null) {
                    RaycastHit hitInfo;
                    Ray ray = new Ray(transform.position, item.transform.position - transform.position);
                    int layerMask = ~(1 << 8); // Anything not invisible. Index 8 is the invisible layer.
                    // Debug.DrawRay(transform.position, item.transform.position-transform.position, Color.cyan);
                    if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask)) {
                        if (hitInfo.transform.gameObject == item) {
                            /*
                            if (!memory.sensoryMemory.iconicMemory.Contains(visualStimulus.data)) {
                                memory.sensoryMemory.iconicMemory.Add(visualStimulus.data);
                                //Debug.Log(gameObject.name + " can see " + item.name + ".");
                            }
                        } else {
                            if (memory.sensoryMemory.iconicMemory.Contains(visualStimulus.data)) {
                                memory.sensoryMemory.iconicMemory.Remove(visualStimulus.data);
                                //Debug.Log(gameObject.name + " can no longer see " + item.name + " because it is obscured by " + hitInfo.transform.gameObject.name + ".");
                            }
                             */
                        }
                    }
                }
            }
        }

        void OnTriggerEnter(Collider target) {
            if (target.gameObject.layer != 8) {
                objectsWithinViewArea.Add(target.gameObject);
            }
        }

        void OnCollisionEnter(Collision collisionInfo) {
            Debug.DrawRay(transform.position, collisionInfo.collider.transform.position - transform.position, Color.green, 5);
            //collider.isTrigger = true;
            //rigidbody.velocity = Vector3.zero;
        }

        void OnTriggerExit(Collider target) {
            if (target.gameObject.layer != 8) {
                VisualStimulus visualStimulus = target.gameObject.GetComponent<VisualStimulus>();
                if (visualStimulus != null) {
                    /*
                    if (memory.sensoryMemory.iconicMemory.Contains(visualStimulus.data)) {
                        memory.sensoryMemory.iconicMemory.Remove(visualStimulus.data);
                        //Debug.Log(gameObject.name + " can no longer see " + target.gameObject.name + " because it has gone outside of the line of sight.");
                    }
                     */
                }
                objectsWithinViewArea.Remove(target.gameObject);
            }
        }
    }
}