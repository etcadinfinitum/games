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
        Vector3 delta = new Vector3(0f, 0f, 0f);
        // check for key presses
        if (Input.GetKeyDown(KeyCode.W)) {
            // move up
            delta = new Vector3(0f, 1f, 0f);
        } else if (Input.GetKeyDown(KeyCode.A)) {
            // move left
            delta = new Vector3(-1f, 0f, 0f);
        } else if (Input.GetKeyDown(KeyCode.S)) {
            // move down
            delta = new Vector3(0f, -1f, 0f);
        } else if (Input.GetKeyDown(KeyCode.D)) {
            // move right
            delta = new Vector3(1f, 0f, 0f);
        }
        // perform position offset
        float currDist = Math.Abs(otherPlayer.position.x - me.position.x);
        float currProjDist = Math.Abs(otherPlayer.position.x - (me.position.x + delta.x));
        if (currProjDist + 1 < 15 || currProjDist < currDist) {
            if ((me.position + delta).y > -5 
                && (me.position + delta).y < 4
                && (me.position + delta).x > -7
                && (me.position + delta).x < 26) {
                    rb.MovePosition(me.position + delta);
            }
        }
        Debug.Log("New coordinates for player1: " + me.position + delta);
    }
}
