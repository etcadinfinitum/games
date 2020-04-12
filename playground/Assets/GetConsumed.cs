using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetConsumed : MonoBehaviour
{
    public HealthBarUpdate health;

    // Start is called before the first frame update
    void Start()
    {
        health = GameObject.Find("HealthBar").GetComponent<HealthBarUpdate>();
    }

    void OnTriggerEnter2D(Collider2D otherObj) {
        Debug.Log("Detected star collision with object named " + otherObj.gameObject.name);
        if (health != null && otherObj.gameObject.name == "Player1") {
            float h = health.GetHealthBarValue();
            health.SetHealthBarValue(h + 0.1f);
            Destroy(gameObject, 0.5f);
        } else if (health == null) {
            Debug.Log("ERROR: Health script variable is null.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
