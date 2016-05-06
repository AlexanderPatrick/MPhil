using UnityEngine;
using System.Collections.Generic;
using AlexanderPatrick;

public class God : PersistentSingletonMonoBehaviour<God> {
    /*
    private static God instance;
    public static God Instance {
        get {
            if (instance == null) {
                instance = GameObject.FindObjectOfType<God>(); // Search for an existing one
                if (instance == null) { 
                    GameObject God = new GameObject("God");
                    instance = God.AddComponent<God>(); // Self-Creation
                }
                DontDestroyOnLoad(instance.gameObject); // Immortality
            }
            return instance;
        }
    }
    void Awake() {
        if (this != Instance) {
            Destroy(this.gameObject); // No false Idols
        }
    }
    */

    // Prefabs
    public List<Prefab> prefabs;

	// Use this for initialization
	void Start () {
        // 
        /*for (int i = 0; i < 200; i++) {
            Instantiate(prefabs[0].gameObject, new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100)), Quaternion.identity);
        }*/
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
