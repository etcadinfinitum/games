using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
	
	public float mSpeed = 20f;
    private GameObject[] waypoints = null;
    private int waypointIdx = 0;
    private int label = -1;
		
	// Use this for initialization
	void Start () {
        transform.position = new Vector3(Random.Range(-45.0f, 45.0f), Random.Range(-45.0f, 45.0f), 0f);
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
        // pick starting waypoint at random
        waypointIdx = ((int) Random.Range(0.0f, waypoints.Length)) % waypoints.Length;
		NewDirection();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += (mSpeed * Time.smoothDeltaTime) * transform.up;

		GlobalBehavior globalBehavior = GameObject.Find ("GameManager").GetComponent<GlobalBehavior>();
		
		GlobalBehavior.WorldBoundStatus status =
			globalBehavior.ObjectCollideWorldBound(GetComponent<Renderer>().bounds);
			
		if (status != GlobalBehavior.WorldBoundStatus.Inside) {
			Debug.Log("collided position: " + this.transform.position);
			NewDirection();
		}	
	}

    public void SetLabel(int label) {
        this.label = label;
    }

    void OnTriggerEnter2D(Collider2D otherObj) {
        if (otherObj.gameObject.tag == "waypoint" && 
            otherObj.gameObject == waypoints[waypointIdx]) {
            NewDirection();
        }
    }

    // New direction will be the next waypoint to be visited 
    // by this enemy.
	private void NewDirection() {
        // choose next waypoint index
        waypointIdx = (waypointIdx + 1) % waypoints.Length;
        Vector3 diff = waypoints[waypointIdx].transform.position - transform.position;
		// Vector2 v = Random.insideUnitCircle;
		transform.up = new Vector3(diff.x, diff.y, 0.0f);
	}
}
