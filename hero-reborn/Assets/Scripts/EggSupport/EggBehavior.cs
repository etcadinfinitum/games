using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBehavior : MonoBehaviour
{
    private const float kEggSpeed = 40f;
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        transform.position += transform.up * (kEggSpeed * Time.smoothDeltaTime);

        // Figure out termination
        bool outside = GlobalBehavior.sTheGlobalBehavior.ObjectCollideWorldBound(GetComponent<Renderer>().bounds) == GlobalBehavior.WorldBoundStatus.Outside;
        if (outside) {
            Destroy(gameObject);  // this.gameObject, this is destroying the game object
            GlobalBehavior.sTheGlobalBehavior.DestroyAnEgg();
        }
    }

    void OnTriggerEnter2D(Collider2D otherObj) {
        if (otherObj.gameObject.tag == "waypoint" || 
            otherObj.gameObject.tag == "Enemy") {
            if (otherObj.gameObject.tag == "waypoint") {
                GlobalBehavior.sTheGlobalBehavior.SetWaypointAlpha(otherObj.gameObject);
            }
            GlobalBehavior.sTheGlobalBehavior.DestroyAnEgg();
            Destroy(gameObject);
        }
    }
}
