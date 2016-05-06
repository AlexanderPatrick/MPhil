using UnityEngine;
using System.Collections;
using Stimuli;

public class Metal : MonoBehaviour {
    public AudioClip metalSound;
    private AuralStimulus auralStimulus;
    private AudioSource audioSource;

    void Start() {
        auralStimulus = GetComponentInChildren<AuralStimulus>();
        auralStimulus.data.amplitude = 0;
        audioSource = GetComponentInChildren<AudioSource>();
        audioSource.clip = metalSound;
    }

	public void BeStruck() {
        auralStimulus.data.amplitude = 1000;
        audioSource.Play();
        StartCoroutine(OnSoundEnd(audioSource.clip.length));
        // AuralStimulus.PlayClipAtPoint(metalSound, transform.position); // Something like this but utilises the aural stimulus script as well.
    }

    IEnumerator OnSoundEnd(float soundLength) {
        yield return new WaitForSeconds(soundLength);
        auralStimulus.data.amplitude = 0;
    }
}
