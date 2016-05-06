using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor( typeof(Brain) )]
public class BrainEditor : Editor {

	Brain m_Instance;
	PropertyField[] m_fields;
	
	public void OnEnable() {
		m_Instance = target as Brain;
		m_fields = ExposeProperties.GetProperties(m_Instance);
	}
	
	public override void OnInspectorGUI () {
		if (m_Instance == null) return;
		this.DrawDefaultInspector();
		ExposeProperties.Expose(m_fields);
	}
}
