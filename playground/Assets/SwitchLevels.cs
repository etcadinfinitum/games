using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchLevels : MonoBehaviour
{

    public int nextConsecutiveScene = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void NextLevel() {
        SceneManager.LoadScene(nextConsecutiveScene);
    }

    public void GameOver() {
        SceneManager.LoadScene(5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
