using System.Collections; 
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.UI;

public class GetSmashed : MonoBehaviour
{
    bool active = false;
    public GameObject spawnNextPrefab;
    public Text smashableText;
    public int requiredSmashes = 1;
    public bool canPlantSpawnedStar = false;
    int totalSmashes = 0;

    // Start is called before the first frame update
    void Start()
    {
        smashableText = GameObject.Find("SmashStats").GetComponent<Text>();
        Debug.Log("smashableText: " + smashableText);
    }

    void OnTriggerEnter2D(Collider2D otherObj) {
        Debug.Log("Crate: Detecting initial trigger collision with player named " + otherObj.gameObject.name);
        if (!active && otherObj.gameObject.name == "Player2") {
            active = true;
        }
        if (smashableText != null) {
            smashableText.text = "Current Smashable: " + otherObj.gameObject.tag + " (" + totalSmashes + "/" + requiredSmashes + ")";
        }
    }

    void OnTriggerExit2D(Collider2D otherObj) {
        if (otherObj.gameObject.name == "Player2") {
            active = false;
            if (smashableText != null) {
                smashableText.text = "Current Smashable: N/A (0/0)";
            }
        }
    }

    void OnTriggerStay2D(Collider2D otherObj) {
        // Debug.Log("Crate: Detecting trigger continuation with player named " + otherObj.gameObject.name);
        if (active && Input.GetKeyDown(KeyCode.Return)) {
            // Debug.Log("Detected enter key!");
            totalSmashes++;
            if (smashableText != null) {
                smashableText.text = "Current Smashable: " + otherObj.gameObject.tag + " (" + totalSmashes + "/" + requiredSmashes + ")";
            }
            if (totalSmashes >= requiredSmashes) {
                totalSmashes = 0;
                GameObject newPrefabInst = Instantiate(spawnNextPrefab, transform.position, Quaternion.identity);
                newPrefabInst.transform.position = transform.position;
                newPrefabInst.GetComponent<GetConsumed>().SetPlanted(canPlantSpawnedStar);
                Destroy(gameObject, 0.1f);
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
