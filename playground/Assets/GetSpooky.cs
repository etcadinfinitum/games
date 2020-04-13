using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSpooky : MonoBehaviour
{
    public HealthBarUpdate health;
    // Start is called before the first frame update
    void Start()
    {
        health = GameObject.Find("LevelMetadata").GetComponentInChildren<HealthBarUpdate>();
    }
    void OnTriggerEnter2D(Collider2D otherObj) {
        if (health != null && otherObj.gameObject.tag == "Player") {
            float h = health.GetHealthBarValue();
            health.SetHealthBarValue(h - 0.05f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
