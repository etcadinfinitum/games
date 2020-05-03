using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeroBehavior : MonoBehaviour {

    public EggStatSystem mEggStat = null;
    public float mHeroSpeed = 20f;
    public float kHeroRotateSpeed = 90f/2f; // 90-degrees in 2 seconds
    private bool useMouseControls = false;
    private Camera cam = null;
    // Use this for initialization

    void Start() {
        Debug.Assert(mEggStat != null);
        cam = Camera.main;
        Debug.Assert(cam != null);
    }
    
    // Update is called once per frame
    void Update() {
        // look for mouse binding key
        if (Input.GetKeyDown(KeyCode.M)) {
            useMouseControls = !useMouseControls;
        }
        UpdateMotion();
        BoundPosition();
        ProcessEggSpwan();
    }

    private void UpdateMotion() {
        if (!useMouseControls) {
            mHeroSpeed += Input.GetAxis("Vertical");
            transform.position += transform.up * (mHeroSpeed * Time.smoothDeltaTime);
            transform.Rotate(Vector3.forward, -1f * Input.GetAxis("Horizontal") *
                                        (kHeroRotateSpeed * Time.smoothDeltaTime));
        } else {
            // follow mouse position
            Debug.Log("Mouse position: " + Input.mousePosition);
            Vector3 newPos = cam.ScreenToWorldPoint(Input.mousePosition);
            newPos.z = transform.position.z;
            transform.position = newPos;
        }
    }

    private void BoundPosition() {
        GlobalBehavior.WorldBoundStatus status = GlobalBehavior.sTheGlobalBehavior.ObjectCollideWorldBound(GetComponent<Renderer>().bounds);
        switch (status) {
            case GlobalBehavior.WorldBoundStatus.CollideBottom:
            case GlobalBehavior.WorldBoundStatus.CollideTop:
                transform.up = new Vector3(transform.up.x, -transform.up.y, 0.0f);
                break;
            case GlobalBehavior.WorldBoundStatus.CollideLeft:
            case GlobalBehavior.WorldBoundStatus.CollideRight:
                transform.up = new Vector3(-transform.up.x, transform.up.y, 0.0f);
                break;
        }
    }

    private void ProcessEggSpwan() {
        if (mEggStat.CanSpawn()) {
            if (Input.GetKeyDown(KeyCode.Space))
                mEggStat.SpawnAnEgg(transform.position, transform.up);
        }
    }
}
