using UnityEngine;
public class SpawnStars : MonoBehaviour 
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject starPrefab;
    int starLimit = 15;

    void Start()
    {
        InvokeRepeating("MakeNewStar", 1.0f, 7.0f);
    }

    void MakeNewStar() {
        if (starPrefab != null && starLimit > 0) {
            GameObject instance = Instantiate(starPrefab);
            // choose random location not already occupied to place it
            instance.GetComponent<Transform>().position = new Vector3(Random.Range(-8f, 25f), Random.Range(-4f, 3f), 0f);
            Debug.Log("Made new star!");
            starLimit--;
        }
    }
}
