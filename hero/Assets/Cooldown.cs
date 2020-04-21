using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour {

    private Image bar;
    private bool active = false;
    private float dur;

    // Start is called before the first frame update
    void Start() {
        bar = GetComponent<Image>();
        bar.fillAmount = 0f;
    }

    public void StartCooldown(float t) {
        if (t > 0f) {
            active = true;
            dur = t;
            bar.fillAmount = 1.0f;
        }
    }

    public bool GetActiveCooldown() {
        return active;
    }
        
    void Update() {
        if (active) {
            bar.fillAmount -= Time.deltaTime / dur;
            if (bar.fillAmount <= 0) {
                active = false;
                bar.fillAmount = 0;
            }
        }
    }

}
