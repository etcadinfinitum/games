using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeroBehavior : MonoBehaviour {

    // public EggStatSystem mEggStat = null;
    private Rigidbody2D rb;
    private Camera cam;
    private float heroSpeed = 20f;
    private const float rotationSpeed = 1f;
    private Vector3 direction;
    private Vector3 rotationAxis;
    private Vector3 basisVector;
    private bool turnedLast = false;

    // Use this for initialization

    void Start () {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0f, 20f);
        // initialize to move up in Y direction
        direction = new Vector3(0f, 10f, 0f);
        // rotation axis is orthogonal to the XY plane
        rotationAxis = new Vector3(0f, 0f, 1f);
        // basis vector is the standard 0deg plane in trig
        basisVector = new Vector3(1f, 0f, 0f);
    }
	
    // Update is called once per frame
    void Update () {
        UpdateSpeed();
        UpdateMotion();
        ProcessEggSpawn();
    }

    private void UpdateSpeed() {
        if (Input.GetKey(KeyCode.UpArrow)) {
            heroSpeed += 5;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            heroSpeed -= 5;
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            heroSpeed = 0;
        }
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
        if (!turnedLast) {
            turnedLast = resolveHeroTurns(screenPoint);
        } else {
            Debug.Log("TurnedLast is true");
            if (screenPoint.x > 0.05 && screenPoint.x < 0.95 && screenPoint.y > 0.05 && screenPoint.y < 0.95) {
                turnedLast = false;
            }
        }
    }

    private bool resolveHeroTurns(Vector3 screenPoint) {
        bool changed = false;
        if (screenPoint.x < 0.05 || screenPoint.x > 0.95) {
            Debug.Log("Screenpoint: " + screenPoint);
            // rotate
            Debug.Log("Rotating to reflect of off LHS/RHS sides");
            Debug.Log("Old rotation/vel: " + rb.rotation + " " + rb.velocity);
            rb.MoveRotation(-rb.rotation);
            Debug.Log("New rotation/vel: " + rb.rotation + " " + rb.velocity);
            changed = true;
        }
        if (screenPoint.y < 0.05 || screenPoint.y > 0.95) {
            Debug.Log("Screenpoint: " + screenPoint);
            Debug.Log("Rotating to reflect of off top/bottom");
            Debug.Log("Old rotation/vel: " + rb.rotation + " " + rb.velocity);
            // rotate
            if (rb.rotation <= 0) {
                rb.MoveRotation(-180 - rb.rotation);
            } else {
                rb.MoveRotation(180 - rb.rotation);
            }
            Debug.Log("New rotation/vel: " + rb.rotation + " " + rb.velocity);
            changed = true;
        }
        return changed;
    }

    private void ProcessEggSpawn() {

    }
}
