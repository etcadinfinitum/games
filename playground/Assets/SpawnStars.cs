using UnityEngine;
public class SpawnStars : MonoBehaviour 
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject starPrefab;
    public int starLimit = 15;
    public bool plantPlusConsume = false;

    void Start()
    {
        InvokeRepeating("MakeNewStar", 1.0f, 7.0f);
    }

    void MakeNewStar() {
        if (starPrefab != null && starLimit > 0) {
            GameObject instance = Instantiate(starPrefab);
            instance.GetComponent<GetConsumed>().SetPlanted(plantPlusConsume);
            // choose random location not already occupied to place it
            instance.GetComponent<Transform>().position = new Vector3(Random.Range(-8f, 25f), Random.Range(-4f, 3f), 0f);
            Debug.Log("Made new star!");
            starLimit--;
        } else {
            Debug.Log("no more stars to spawn? remaining: " + starLimit);
        }
    }
}
