using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;           // Math.Abs

public class MovePlayer1 : MonoBehaviour
{
    Rigidbody2D rb;
    Transform otherPlayer;
    Transform me;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        otherPlayer = GameObject.Find("Player2").GetComponent<Transform>();
        me = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 delta = new Vector2(0f, 0f);
        // check for key presses
        if (Input.GetKeyDown(KeyCode.W)) {
            // move up
            delta = new Vector2(0f, 1f);
        } else if (Input.GetKeyDown(KeyCode.A)) {
            // move left
            delta = new Vector2(-1f, 0f);
        } else if (Input.GetKeyDown(KeyCode.S)) {
            // move down
            delta = new Vector2(0f, -1f);
        } else if (Input.GetKeyDown(KeyCode.D)) {
            // move right
            delta = new Vector2(1f, 0f);
        }
        // rb.MovePosition(rb.position + delta);
        if (Math.Abs(otherPlayer.position.x - me.position.x + delta.x) + 1 < 15) {
            if ((rb.position + delta).y > -5 
                && (rb.position + delta).y < 4
                && (rb.position + delta).x > -7
                && (rb.position + delta).x < 26) {
                    rb.MovePosition(rb.position + delta);
            }
        } else {
            // move player back into movement range
            delta.x = (delta.x * -2f);
            rb.MovePosition(rb.position + delta);
        }
    }
}
