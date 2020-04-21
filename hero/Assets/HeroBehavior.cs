using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeroBehavior : MonoBehaviour {

    // public EggStatSystem mEggStat = null;
    private Rigidbody2D rb;
    private Camera cam;
    private GameObject eggMan;
    private Slider cooldownSlider;
    private Cooldown cooldownBar;
    private float heroSpeed = 20f;
    private const float rotationSpeed = 1f;
    private Vector3 direction;
    private bool resolveXbounds = false;
    private bool resolveYbounds = false;
    private float cooldownTime = 0f;

    // Use this for initialization

    void Start () {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        eggMan = GameObject.Find("EggiBoi");
        cooldownSlider = GameObject.Find("CooldownSlider").GetComponent<Slider>();
        cooldownBar = GameObject.Find("CooldownBar").GetComponentInChildren<Cooldown>();
        rb.velocity = new Vector2(0f, 20f);
        // initialize to move up in Y direction
        direction = new Vector3(0f, 10f, 0f);
    }
	
    // Update is called once per frame
    void Update () {
        CheckIfQuit();
        UpdateSpeed();
        UpdateMotion();
        ProcessEggSpawn();
    }

    private void CheckIfQuit() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            #if (UNITY_EDITOR)
                UnityEditor.EditorApplication.isPlaying = false;
            #elif (UNITY_STANDALONE) 
                Application.Quit();
            #elif (UNITY_WEBGL)
                Application.OpenURL("about:blank");
            #endif
        }
    }

    private void UpdateSpeed() {
        if (Input.GetKey(KeyCode.UpArrow)) {
            heroSpeed += 1f;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            heroSpeed -= 1f;
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            heroSpeed = 0f;
        }
        Debug.Log("Hero speed: " + heroSpeed);
    }

    private void UpdateMotion() {
        // check for rotation & update
        if (Input.GetKey(KeyCode.A)) {
            // rotate "left"
            rb.MoveRotation(rb.rotation + rotationSpeed);
        }
        if (Input.GetKey(KeyCode.D)) {
            // rotate "right"
            rb.MoveRotation(rb.rotation - rotationSpeed);
        }
        // update speed
        rb.velocity = (new Vector2(-1 * Mathf.Sin(rb.rotation * Mathf.Deg2Rad), Mathf.Cos(rb.rotation * Mathf.Deg2Rad)) * heroSpeed);
    }

    void FixedUpdate() {
        // check if out of bounds; if so, reverse that velocity component
        Vector3 screenPoint = cam.WorldToViewportPoint(transform.position);
        // Debug.Log("Screenpoint: " + screenPoint);
        if (!(resolveXbounds || resolveYbounds)) {
            resolveHeroTurns(screenPoint);
        } else {
            Debug.Log("One or both of the boundaries needs to be resolved");
            if (screenPoint.x > 0.05 && screenPoint.x < 0.95 && screenPoint.y > 0.05 && screenPoint.y < 0.95) {
                resolveXbounds = false;
                resolveYbounds = false;
            }
        }
    }

    private void resolveHeroTurns(Vector3 screenPoint) {
        if (screenPoint.x < 0.05 || screenPoint.x > 0.95) {
            // rotate
            rb.MoveRotation(-rb.rotation);
            resolveXbounds = true;
        }
        if (screenPoint.y < 0.05 || screenPoint.y > 0.95) {
            // rotate
            if (rb.rotation <= 0) {
                rb.MoveRotation(-180 - rb.rotation);
            } else {
                rb.MoveRotation(180 - rb.rotation);
            }
            resolveYbounds = true;
        }
    }

    private void ProcessEggSpawn() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            // check if cooldown is over
            if (!cooldownBar.GetActiveCooldown()) {
                // add eeg
                GameObject newEggMan = Instantiate(eggMan);
                newEggMan.GetComponent<Transform>().position = transform.position;
                newEggMan.AddComponent<EggBehavior>();
                // trigger cooldown bar updates
                cooldownBar.StartCooldown(cooldownSlider.value);
            }
        }
    }
}
