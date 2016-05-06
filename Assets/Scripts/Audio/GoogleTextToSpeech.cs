using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
 
 /// <summary>
 /// <author>Jefferson Reis; Alexander Patrick</author>
 /// <explanation>Works only on Android, or platform that supports mp3 files. To test, change the platform to Android.</explanation>
 /// </summary>
public class GoogleTextToSpeech {
    public static IEnumerator GetSpeech(string text, Action<AudioClip> onReceiveSpeech) {
        string url = SanitiseURL(text);
        WWW www = new WWW(url);
        yield return www;
        onReceiveSpeech( www.GetAudioClip(false, false, AudioType.MPEG) );
    }

    public static IEnumerator Say(string text, Dictionary<string,AudioClip> clips, AudioSource audioSource) {
        string url = SanitiseURL(text);
        WWW www = new WWW (url);
        yield return www;
        clips[text] = www.GetAudioClip (false, false, AudioType.MPEG);
        audioSource.clip = clips[text];
        audioSource.Play();
    }

    public static IEnumerator Say(string text) {
        string url = SanitiseURL(text);
        WWW www = new WWW(url);
        yield return www;
        AudioSource.PlayClipAtPoint(www.GetAudioClip(false, false, AudioType.MPEG),Vector3.zero);
    }

    private static string SanitiseURL(string input) {
        // Replace the "spaces" with "%20" so the link can be interpreted
        Regex rgx = new Regex("\\s+");
        string sanitisedText = rgx.Replace(input, "%20");
        return "http://translate.google.com/translate_tts?tl=en&q=" + sanitisedText;
    }
}
