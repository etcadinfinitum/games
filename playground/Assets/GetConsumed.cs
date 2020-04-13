using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetConsumed : MonoBehaviour
{
    bool planted = false;
    public HealthBarUpdate health;
    public GameObject spawnPlantPrefab;
    bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        health = GameObject.Find("LevelMetadata").GetComponentInChildren<HealthBarUpdate>();
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
                active = true;
            }
        } else if (health == null) {
            Debug.Log("ERROR: Health script variable is null.");
        }
    }

    void OnTriggerExit2D(Collider2D otherObj) {
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active) {
            Debug.Log("Trigger stayed for star; waiting for keyboard input...");
            // Debug.Log("Crate: Detecting trigger continuation with player named " + otherObj.gameObject.name);
            if (Input.GetKeyDown(KeyCode.E)) {
                Debug.Log("Detected E for eat!");
                float h = health.GetHealthBarValue();
                health.SetHealthBarValue(h + 0.05f);
                Destroy(gameObject, 0.1f);
            } else if (Input.GetKeyDown(KeyCode.Z)) {
                Debug.Log("Detected Z for plant!");
                // plant the star
                GameObject newBush = Instantiate(spawnPlantPrefab, transform.position, Quaternion.identity);
                newBush.SetActive(true);
                newBush.AddComponent<GrowStars>();
                newBush.transform.position = gameObject.transform.position;
                Destroy(gameObject, 0.1f);
            }
        }
    }
}
