using UnityEngine;

public class ToneGenerator : MonoBehaviour {
    public int position = 0;
    public int sampleRate = 0;
    public float frequency = 440;
    void Start() {
        AudioClip myClip = AudioClip.Create("MySinoid", 44100, 1, 44100, true, OnAudioRead, OnAudioSetPosition);
        sampleRate = AudioSettings.outputSampleRate;
        GetComponent<AudioSource>().clip = myClip;
        GetComponent<AudioSource>().Play();
    }
    void OnAudioRead(float[] data) {
        Debug.Log("Reading Audio: Size=" + data.Length);
        int count = 0;
        while (count < data.Length) {
            data[count] = Mathf.Sign(Mathf.Sin(2 * Mathf.PI * frequency * position / sampleRate));
            position++;
            count++;
        }
    }
    void OnAudioSetPosition(int newPosition) {
        Debug.Log("Setting Audio Position to " + newPosition);
        position = newPosition;
    }
}
