using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSmashed : MonoBehaviour
{
    bool active = false;
    public GameObject spawnNextPrefab;
    public GameObject helpTextPrefab;
    public int requiredSmashes = 1;
    public bool canPlantSpawnedStar = false;
    int totalSmashes = 0;
    GameObject helpTextItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D otherObj) {
        Debug.Log("Crate: Detecting initial trigger collision with player named " + otherObj.gameObject.name);
        if (!active && otherObj.gameObject.name == "Player2") {
            active = true;
        }
        if (active && helpTextPrefab != null) {
            // TODO: show prompt for "press enter to smash"
            // TODO: show health bar
            helpTextItem = Instantiate(helpTextPrefab, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
        }
    }

    void OnTriggerExit2D(Collider2D otherObj) {
        if (otherObj.gameObject.name == "Player2") {
            active = false;
        }
        Destroy(helpTextItem, 0.1f);
    }

    void OnTriggerStay2D(Collider2D otherObj) {
        // Debug.Log("Crate: Detecting trigger continuation with player named " + otherObj.gameObject.name);
        if (active && Input.GetKeyDown(KeyCode.Return)) {
            // Debug.Log("Detected enter key!");
            totalSmashes++;
            if (totalSmashes >= requiredSmashes) {
                GameObject newPrefabInst = Instantiate(spawnNextPrefab, transform.position, Quaternion.identity);
                newPrefabInst.transform.position = transform.position;
                newPrefabInst.GetComponent<GetConsumed>().SetPlanted(canPlantSpawnedStar);
                Destroy(gameObject, 0.1f);
                Destroy(helpTextItem);
            } else {
                Debug.Log("More hits are required to destroy this box. Required:Given is " + totalSmashes + ":" + requiredSmashes);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
