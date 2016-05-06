using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneManager : MonoBehaviour {
    public List<AbstractScene> scenes;

	// Use this for initialization
	void Start () {
        scenes = new List<AbstractScene>();
        scenes.Add(new AlbertScene());
        scenes[0].Setting();
        StartCoroutine(scenes[0].Plot());
	}
}
