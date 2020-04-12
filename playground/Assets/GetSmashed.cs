using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSmashed : MonoBehaviour
{
    bool active = false;
    public GameObject spawnNextPrefab;
    public GameObject helpTextPrefab;
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
            // show prompt for "press enter to smash"
            helpTextItem = Instantiate(helpTextPrefab, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
        }
    }

    void OnTriggerExit2D(Collider2D otherObj) {
        if (otherObj.gameObject.name == "Player2") {
            active = false;
        }
    }

    void OnTriggerStay2D(Collider2D otherObj) {
        Debug.Log("Crate: Detecting trigger continuation with player named " + otherObj.gameObject.name);
        if (active && Input.GetKeyDown(KeyCode.Return)) {
            Debug.Log("Detected enter key!");
            GameObject newPrefabInst = Instantiate(spawnNextPrefab, transform.position, Quaternion.identity);
            newPrefabInst.transform.position = transform.position;
            Destroy(gameObject, 0.1f);
            Destroy(helpTextItem);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
