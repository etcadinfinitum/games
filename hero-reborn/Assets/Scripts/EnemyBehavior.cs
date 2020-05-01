using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
    
    public float mSpeed = 20f;
    private GameObject[] waypoints = null;
    private int waypointIdx = 0;
    private int label = -1;
    private bool random = false;
        
    // Use this for initialization
    void Start() {
        transform.position = new Vector3(Random.Range(-45.0f, 45.0f), Random.Range(-45.0f, 45.0f), 0f);
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
        // pick starting waypoint at random
        waypointIdx = ((int) Random.Range(0.0f, waypoints.Length)) % waypoints.Length;
        NewDirection();
    }
    
    // Update is called once per frame
    void Update() {
        transform.position += (mSpeed * Time.smoothDeltaTime) * transform.up;

        GlobalBehavior globalBehavior = GameObject.Find ("GameManager").GetComponent<GlobalBehavior>();
        
        GlobalBehavior.WorldBoundStatus status =
            globalBehavior.ObjectCollideWorldBound(GetComponent<Renderer>().bounds);
            
        if (status != GlobalBehavior.WorldBoundStatus.Inside) {
            Debug.Log("collided position: " + this.transform.position);
            NewDirection();
        }
        
        if (Input.GetKeyDown(KeyCode.J)) {
            Debug.Log("Toggling random waypoint for Enemy " + label);
            // toggle random waypoint
            ToggleRandomWaypoint();
        }
    }

    void ToggleRandomWaypoint() {
        // invert the random bool value
        random = !random;
    }

    public void InitializeEnemyState(int label, bool random) {
        this.label = label;
        this.random = random;
        // Debug.Log("Waypoint for Enemy " + this.label + " is random?: " + this.random);
    }

    void OnTriggerEnter2D(Collider2D otherObj) {
        if (otherObj.gameObject.tag == "waypoint" && 
            otherObj.gameObject == waypoints[waypointIdx]) {
            NewDirection();
        } else if (otherObj.gameObject.tag == "egg" || 
                   otherObj.gameObject.name == "Hero") {
            // Debug.Log("Enemy collided with item with name " + otherObj.gameObject.name);
            GlobalBehavior g = GameObject.Find("GameManager").GetComponent<GlobalBehavior>();
            g.KillAnEnemy(label);
            Destroy(gameObject);
        }
    }

    // New direction will be the next waypoint to be visited 
    // by this enemy.
    private void NewDirection() {
        int oldWaypoint = waypointIdx;
        if (random) {
            // choose random waypoint which is different than current waypoint
            while (waypointIdx == oldWaypoint) {
                waypointIdx = ((int) Random.Range(0.0f, 350.0f)) % waypoints.Length;
            }
        } else {
            // choose next sequential waypoint index
            waypointIdx = (waypointIdx + 1) % waypoints.Length;
        }
        Vector3 diff = waypoints[waypointIdx].transform.position - transform.position;
        // Vector2 v = Random.insideUnitCircle;
        transform.up = new Vector3(diff.x, diff.y, 0.0f);
    }
}
