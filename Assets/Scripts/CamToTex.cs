using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class CamToTex : MonoBehaviour {
	public Camera cam;
	public Texture2D tex;
	
	// Use this for initialization
	void Start () {
		StartCoroutine( eyeRoutine() );
	}
	
	private IEnumerator eyeRoutine() {
		yield return new WaitForEndOfFrame();
		tex = CaptureImage(cam,Screen.width,Screen.height);
	}
	
	public Texture2D CaptureImage(Camera camera, int width, int height, Nullable<Quaternion> rotation = null, Nullable<Vector3> position = null, bool outputToPNG = false) {
		Texture2D captured = new Texture2D (width, height,  TextureFormat.ARGB32, false);
		if (rotation != null) {
			camera.transform.rotation = rotation.Value;
		}
		
		if (position != null) {
			camera.transform.position = position.Value;
		}
		
		camera.Render();
		RenderTexture.active = camera.targetTexture;
		captured.ReadPixels(new Rect(0,0,width,height),0,0);
		captured.Apply();
		RenderTexture.active = null;
		
		if (outputToPNG) {
			// Encode texture into PNG 
			byte[] bytes = captured.EncodeToPNG();
			
			// For testing purposes, also write to a file in the project folder // 
			File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);
		}
		
		return captured;
	}
}
