using UnityEngine;
using System.Collections.Generic;

namespace Behaviours {
    [RequireComponent(typeof(AudioSource))]
    public class Voice : MonoBehaviour {
        private AudioSource audioSource;
        public List<AudioClip> clips;
        public Dictionary<string, AudioClip> speech;

        public bool IsSpeaking {
            get { return audioSource != null ? audioSource.isPlaying : false; }
        }

        void Awake() {
            speech = new Dictionary<string, AudioClip>();
        }

        void Start() {
            audioSource = GetComponent<AudioSource>();
        }

        private void playSpeechClip(string text) {
            if (audioSource != null) {
                audioSource.clip = speech[text];
                audioSource.Play();
            }
        }

        public void Say(int clipIndex) {
            if (audioSource != null) {
                audioSource.clip = clips[clipIndex];
                audioSource.Play();
            }
        }

        public void Say(string text) {
            if (speech.ContainsKey(text)) {
                playSpeechClip(text);
            } else {
                StartCoroutine(GoogleTextToSpeech.GetSpeech(text, (AudioClip audio) => { speech[text] = audio; playSpeechClip(text); }));
            }
        }

        public void SayHello() {
            Say("Hello");
        }
    }
}