using UnityEngine;
using System;
using System.Collections;

namespace Stimuli {
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class VisualStimulus : MonoBehaviour {
        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;

        public void Start() {
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();
            data.colour = meshRenderer.material.color;
            data.shape = meshFilter.mesh;
        }

        public void Update() {
            data.position = transform.position;
        }

        public Vector3 Position {
            get {
                return transform.position;
            }
        }

        public Color Colour {
            get {
                return meshRenderer.material.color;
            }
        }

        public Mesh Shape {
            get {
                return meshFilter.mesh;
            }
        }

        public VisualStimulusData data; // For Backward Compatibility
    }
}