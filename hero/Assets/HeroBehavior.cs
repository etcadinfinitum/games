using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeroBehavior : MonoBehaviour {

    // public EggStatSystem mEggStat = null;
    private float mHeroSpeed = 5f;
    private const float kHeroRotateSpeed = 22f; 

    // Use this for initialization

    void Start () {

    }
	
    // Update is called once per frame
    void Update () {
      UpdateMotion();
      ProcessEggSpwan();
    }

    private void UpdateMotion() {
    }
    
    private void ProcessEggSpwan() {

    }
}
