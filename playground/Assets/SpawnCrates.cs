using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnCrates : MonoBehaviour {
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject cratePrefab;
    public int crateLimit = 15;
    public bool hiddenStarCanBePlanted = false;

    void Start()
    {
        InvokeRepeating("MakeNewCrate", 3.0f, 9.0f);
    }

    void MakeNewCrate() {
        if (cratePrefab != null && crateLimit > 0) {
            GameObject instance = Instantiate(cratePrefab);
            // choose random location not already occupied to place it
            instance.GetComponent<Transform>().position = new Vector3(Random.Range(-8f, 25f), Random.Range(-4f, 3f), 0f);
            instance.GetComponent<GetSmashed>().canPlantSpawnedStar = hiddenStarCanBePlanted;
            Debug.Log("Made new crate!");
            crateLimit--;
        }
    }
}
