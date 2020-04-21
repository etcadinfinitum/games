using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    private Rigidbody2D hero;
    private Camera cam;
    private float eggSpeed = 40f;
    private float startTime;

    // Start is called before the first frame update
    void Start() {
        cam = Camera.main;
        rb = gameObject.GetComponent<Rigidbody2D>();
        hero = GameObject.Find("Hero").GetComponent<Rigidbody2D>();
        startTime = Time.time;
        rb.MoveRotation(hero.rotation);
        transform.position = hero.GetComponent<Transform>().position;
        rb.velocity = new Vector2(-1 * Mathf.Sin(rb.rotation * Mathf.Deg2Rad), Mathf.Cos(rb.rotation * Mathf.Deg2Rad)) * eggSpeed;
    }

    // Update is called once per frame
    void Update() {
        // check if alive for 1 second
        float now = Time.time;
        if (now - startTime >= 1.0f) {
            Destroy(gameObject);
        }
        // check if outside camera bounds
        Vector3 screenPoint = cam.WorldToViewportPoint(transform.position);
        if (screenPoint.x < 0 && screenPoint.x > 1 && screenPoint.y < 0 && screenPoint.y > 1) {
            Destroy(gameObject);
        }
    }
}
