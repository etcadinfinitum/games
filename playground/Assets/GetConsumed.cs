using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetConsumed : MonoBehaviour
{
    bool planted = false;
    public HealthBarUpdate health;
    public GameObject spawnPlantPrefab;

    // Start is called before the first frame update
    void Start()
    {
        health = GameObject.Find("HealthBar").GetComponent<HealthBarUpdate>();
        if (planted) {
            spawnPlantPrefab = GameObject.Find("BushTemplate");
        }
    }

    public void SetPlanted(bool planted) {
        this.planted = planted;
    }

    void OnTriggerEnter2D(Collider2D otherObj) {
        // Debug.Log("Detected star collision with object named " + otherObj.gameObject.name);
        if (health != null && otherObj.gameObject.name == "Player1") {
            if (!planted) {
                Debug.Log("Ate star!");
                float h = health.GetHealthBarValue();
                health.SetHealthBarValue(h + 0.1f);
                Destroy(gameObject, 0.5f);
            } else {
                // TODO: prompt for consume or plant
            }
        } else if (health == null) {
            Debug.Log("ERROR: Health script variable is null.");
        }
    }

    void OnTriggerStay2D(Collider2D otherObj) {
        if (!planted) return;
        // Debug.Log("Crate: Detecting trigger continuation with player named " + otherObj.gameObject.name);
        if (otherObj.gameObject.name == "Player1" && Input.GetKeyDown(KeyCode.E)) {
            Debug.Log("Detected E for eat!");
            float h = health.GetHealthBarValue();
            health.SetHealthBarValue(h + 0.05f);
            Destroy(gameObject, 0.1f);
        } else if (otherObj.gameObject.name == "Player1" && Input.GetKeyDown(KeyCode.Z)) {
            Debug.Log("Detected Z for plant!");
            // TODO: plant the star
            GameObject newBush = Instantiate(spawnPlantPrefab, transform.position, Quaternion.identity);
            newBush.SetActive(true);
            newBush.transform.position = gameObject.transform.position;
            Destroy(gameObject, 0.1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
