using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GlobalBehavior : MonoBehaviour {
    public static GlobalBehavior sTheGlobalBehavior = null;
    public EggStatSystem mEggStat = null;
    public GameObject enemyPrefab = null;

    public Text mGameStateEcho = null;  // Defined in UnityEngine.UI

    private GameObject[] waypoints = null;
    private int[] waypointAlphaDiff = null;
    private bool waypointsActive = true;
    private Vector3[] waypointPosition = null;

    private bool enemiesAreRandom = false;
    private int enemiesKilled = 0;

    private int eggCount = 0;

    private bool useMouseControls = false;

    #region World Bound support
    private Bounds mWorldBound;  // this is the world bound
    private Vector2 mWorldMin;  // Better support 2D interactions
    private Vector2 mWorldMax;
    private Vector2 mWorldCenter;
    private Camera mMainCamera;
    #endregion 

    // This is called before any Start()
    //     https://docs.unity3d.com/Manual/ExecutionOrder.html
    void Awake() {
        // just so we know
    }

    // Use this for initialization
    void Start() {
        Debug.Assert(mEggStat != null);

        GlobalBehavior.sTheGlobalBehavior = this;  // Singleton pattern

        // load enemy prefab
        enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");
        Debug.Assert(enemyPrefab != null);

        #region world bound support
        mMainCamera = Camera.main; // This is the default main camera
        mWorldBound = new Bounds(Vector3.zero, Vector3.one);
        UpdateWorldWindowBound();
        #endregion

        // spawn and place 10 enemies
        for (int i = 0; i < 10; i++) {
            GameObject newEnemy = Instantiate(enemyPrefab);
            newEnemy.GetComponent<EnemyBehavior>().InitializeEnemyState(i + 1, enemiesAreRandom);
        }

        // get the waypoint references
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
        Debug.Assert(waypoints != null && waypoints.Length != 0);
        waypointAlphaDiff = new int[waypoints.Length];  // all elements are initialized to zero
        waypointPosition = new Vector3[waypoints.Length];
        for (int i = 0; i < waypointPosition.Length; i++) {
            waypointPosition[i] = waypoints[i].transform.position;
        }
    }

    void Update() {
        /*
        mMainCamera.transform.position += 0.1f * Vector3.one;
        mMainCamera.orthographicSize += 1.0f;
        */
        
        // hide waypoints if desired
        if (Input.GetKeyDown(KeyCode.H)) {
            waypointsActive = !waypointsActive;
            foreach (GameObject go in waypoints) {
                go.GetComponent<SpriteRenderer>().enabled = waypointsActive;
            }
        }

        // keep track of enemy randomness
        if (Input.GetKeyDown(KeyCode.J)) {
            enemiesAreRandom = !enemiesAreRandom;
        }

        // look for mouse binding key
        if (Input.GetKeyDown(KeyCode.M)) {
            useMouseControls = !useMouseControls;
        }

        UpdateGameState();
    }

    public void SetWaypointAlpha(GameObject waypoint) {
        for (int i = 0; i < waypoints.Length; i++) {
            if (waypoints[i] == waypoint) {
                waypointAlphaDiff[i] += 1;
                if (waypointAlphaDiff[i] >= 4) {
                    // set to 0
                    waypointAlphaDiff[i] = 0;
                    // move waypoint
                    Vector3 newPos = new Vector3(
                        Random.Range(waypointPosition[i].x + 15f, waypointPosition[i].x - 15f), 
                        Random.Range(waypointPosition[i].y + 15f, waypointPosition[i].y - 15f), 
                        0f
                    );
                    waypoints[i].transform.position = newPos;

                    // notify enemies that waypoint has been moved
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    Debug.Assert(enemies != null && enemies.Length != 0);
                    foreach (GameObject enemy in enemies) {
                        enemy.SendMessage("UpdateDestination", waypoints[i]);
                    }
                }
                Color tmp = waypoint.GetComponent<SpriteRenderer>().color;
                tmp.a = (4f - waypointAlphaDiff[i]) / 4f;
                waypoint.GetComponent<SpriteRenderer>().color = tmp;
            }
        }
    }

    #region Game Window World size bound support
    public enum WorldBoundStatus {
        CollideTop,
        CollideLeft,
        CollideRight,
        CollideBottom,
        Outside,
        Inside
    };
    
    /// <summary>
    /// This function must be called anytime the MainCamera is moved, or changed in size
    /// </summary>
    public void UpdateWorldWindowBound() {
        // get the main 
        if (null != mMainCamera) {
            float maxY = mMainCamera.orthographicSize;
            float maxX = mMainCamera.orthographicSize * mMainCamera.aspect;
            float sizeX = 2 * maxX;
            float sizeY = 2 * maxY;
            float sizeZ = Mathf.Abs(mMainCamera.farClipPlane - mMainCamera.nearClipPlane);
            
            // Make sure z-component is always zero
            Vector3 c = mMainCamera.transform.position;
            c.z = 0.0f;
            mWorldBound.center = c;
            mWorldBound.size = new Vector3(sizeX, sizeY, sizeZ);

            mWorldCenter = new Vector2(c.x, c.y);
            mWorldMin = new Vector2(mWorldBound.min.x, mWorldBound.min.y);
            mWorldMax = new Vector2(mWorldBound.max.x, mWorldBound.max.y);
        }
    }

    public Vector2 WorldCenter { get { return mWorldCenter; } }
    public Vector2 WorldMin { get { return mWorldMin; } } 
    public Vector2 WorldMax { get { return mWorldMax; } }
    
    public WorldBoundStatus ObjectCollideWorldBound(Bounds objBound) {
        WorldBoundStatus status = WorldBoundStatus.Inside;

        if (mWorldBound.Intersects (objBound)) {
            if (objBound.max.x > mWorldBound.max.x)
                status = WorldBoundStatus.CollideRight;
            else if (objBound.min.x < mWorldBound.min.x)
                status = WorldBoundStatus.CollideLeft;
            else if (objBound.max.y > mWorldBound.max.y)
                status = WorldBoundStatus.CollideTop;
            else if (objBound.min.y < mWorldBound.min.y)
                status = WorldBoundStatus.CollideBottom;
            else if ((objBound.min.z < mWorldBound.min.z) || (objBound.max.z > mWorldBound.max.z))
                status = WorldBoundStatus.Outside;
        } else 
            status = WorldBoundStatus.Outside;

        return status;
    }

    public WorldBoundStatus ObjectClampToWorldBound(Transform t) {
        WorldBoundStatus status = WorldBoundStatus.Inside;
        Vector3 p = t.position;

        if (p.x > WorldMax.x) {
            status = WorldBoundStatus.CollideRight;
            p.x = WorldMax.x;
        }
        else if (t.position.x < WorldMin.x) {
            status = WorldBoundStatus.CollideLeft;
            p.x = WorldMin.x;
        }

        if (p.y > WorldMax.y) {
            status = WorldBoundStatus.CollideTop;
            p.y = WorldMax.y;
        }
        else if (p.y < WorldMin.y) {
            status = WorldBoundStatus.CollideBottom;
            p.y = WorldMin.y;
        }

        if ((p.z < mWorldBound.min.z) || (p.z > mWorldBound.max.z)) {
            status = WorldBoundStatus.Outside;
        }

        t.position = p;
        return status;
    }
    #endregion

    #region Game Stat Echo support
    public void DestroyAnEgg() {
        mEggStat.DecEggCount();
    }

    public void KillAnEnemy(int oldLabel) {
        enemiesKilled += 1;
        GameObject newEnemy = Instantiate(enemyPrefab) as GameObject;
        newEnemy.GetComponent<EnemyBehavior>().InitializeEnemyState(oldLabel, enemiesAreRandom);
        
    }

    public void UpdateEggCount(int count) {
        this.eggCount = count;
    }

    public void UpdateGameState() {
        string msg = "Waypoint navigation: " + (enemiesAreRandom ? "Random" : "Sequential") + "\n";
        msg += "Hero navigation: " + (useMouseControls ? "Cursor" : "Keyboard") + "\n";
        msg += "Enemies killed: " + enemiesKilled + "\n";
        msg += "Egg count: " + eggCount;
        mGameStateEcho.text = msg;
    }

    #endregion
}
