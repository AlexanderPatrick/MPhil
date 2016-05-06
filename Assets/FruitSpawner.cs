using UnityEngine;
using System.Collections;

public class FruitSpawner : MonoBehaviour {
    public GameObject fruit;
    public float spawnProbability;
    public float spawnFrequency;
    public float spawnRadius;

    private float timeToSpawn;

	// Use this for initialization
	void Start () {
        if (fruit == null) {
            Debug.LogWarning("No Fruit applied");
            this.enabled = false;
            return;
        }
        timeToSpawn = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (timeToSpawn <= Time.time) {
            timeToSpawn = Time.time + 1 / spawnFrequency;

            if (fruit != null) {
                if (Random.Range(0f, 1f) < spawnProbability) {
                    float x = Random.Range(1, spawnRadius);
                    float z = Random.Range(1, spawnRadius);
                    x *= Random.Range(0, 2) == 1 ? -1 : 1;
                    z *= Random.Range(0, 2) == 1 ? -1 : 1;
                    Instantiate(fruit, transform.position + new Vector3(x, 1, z), Quaternion.identity);
                }
            }
        }
	}
}
