using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetConsumed : MonoBehaviour
{
    public HealthBarUpdate health;

    // Start is called before the first frame update
    void Start()
    {
        health = GameObject.FindGameObjectWithTag("Health").GetComponent<HealthBarUpdate>();
    }

    void OnCollisionEnter2D() {
        if (health != null) {
            float h = health.GetHealthBarValue();
            health.SetHealthBarValue(h + 0.1f);
        }
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
