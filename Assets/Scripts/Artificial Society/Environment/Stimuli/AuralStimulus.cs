using UnityEngine;
using Senses;
using System.Collections.Generic;

namespace Stimuli {
    [RequireComponent(typeof(AudioSource), typeof(SphereCollider))]
    public class AuralStimulus : MonoBehaviour {
        public static void PlayClipAtPoint(AudioClip clip, Vector3 point) {
            GameObject oneshotaudio = new GameObject();
            oneshotaudio.name = "One Shot Audio";
            oneshotaudio.transform.position = point;

            AuralStimulus auralStimulus = oneshotaudio.AddComponent<AuralStimulus>();
            auralStimulus.data.amplitude = 1000;
            auralStimulus.data.frequency = 1;
            auralStimulus.data.position = point;

            AudioSource audioSource = oneshotaudio.GetComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.Play();

            Destroy(oneshotaudio, clip.length); // NUUUUU! Destroy doesn't trigger an OnTriggerExit
        }


        public AuralStimulusData data;

        private AudioSource audioSource;
        private SphereCollider sphereCollider;

        private List<Audition> listeners;

        void Awake() {
            listeners = new List<Audition>();
        }

        void Start() {
            audioSource = GetComponent<AudioSource>();
            audioSource.spatialBlend = 1;
            audioSource.maxDistance = data.amplitude;
            sphereCollider = GetComponent<SphereCollider>();
            sphereCollider.isTrigger = true;
            sphereCollider.radius = data.amplitude;
        }

        void OnTriggerEnter(Collider collider) {
            Audition listener = collider.GetComponent<Audition>();
            if (listener != null) {
                listeners.Add(listener);
            }
        }

        void OnTriggerExit(Collider collider) {
            Audition listener = collider.GetComponent<Audition>();
            if (listener != null) {
                listeners.Remove(listener);
            }
        }

        void OnAudioPlay() {
            foreach (Audition listener in listeners) {
                float distance = Vector3.Distance(transform.position, listener.transform.position);
                listener.Hear(this);
                // Write directly to their listening buffer
                // It would be nice to figure out how to get the waveform for what is being played at that precise time. So we can get super close to synthetic hearing just like the camera snapshot approach for vision.
                // I can't really figure out how to describe sounds in more symbolic forms to apply more concepts of structuralism.
                // Voice/Speech recognition, is the closest but that just turns it into a NLP problem. Then there's notes on a staff for music which could be good for an agent determining if music is pleasing or not.
                // Wow, sentiments based off of a sound. It's exactly what Sir wanted Kemar to do.
            }
        }

        void Update() {
            audioSource.maxDistance = data.amplitude;
            sphereCollider.radius = data.amplitude;
        }

        public Vector3 Position {
            get { return transform.position; }
        }

        public float Amplitude {
            get { return data.amplitude; }
        }

        // Why do I even keep this? I don't even use it.
        public float Frequency {
            get { return data.frequency; }
        }
    }
}