using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnZombies : MonoBehaviour {
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject zPrefab;

    void Start()
    {
        InvokeRepeating("MakeNewZombie", 3.0f, 9.0f);
    }

    void MakeNewZombie() {
        if (zPrefab != null) {
            GameObject instance = Instantiate(zPrefab);
            // choose random location not already occupied to place it
            instance.GetComponent<Transform>().position = new Vector3(Random.Range(-8f, 25f), Random.Range(-4f, 3f), 0f);
            instance.AddComponent<GetSpooky>();
            instance.AddComponent<Wander>();
            Debug.Log("Made new crate!");
        }
    }
    
}
