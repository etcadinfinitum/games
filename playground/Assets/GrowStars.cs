﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowStars : MonoBehaviour
{
    public float seconds = 5f;
    public GameObject spawnStarPrefab;
    public bool growPlantableStars = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("GrowThoseStars");
    }

    IEnumerator GrowThoseStars() {
        Debug.Log("Entering coroutine for star plants...");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Populating stars after wait timer...");
        GameObject star1 = Instantiate(spawnStarPrefab, transform.position + new Vector3(-0.5f, 0f, 0f), Quaternion.identity);
        star1.AddComponent<GetConsumed>();
        star1.GetComponent<GetConsumed>().SetPlanted(growPlantableStars);
        GameObject star2 = Instantiate(spawnStarPrefab, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
        star2.AddComponent<GetConsumed>();
        star2.GetComponent<GetConsumed>().SetPlanted(growPlantableStars);
        GameObject star3 = Instantiate(spawnStarPrefab, transform.position + new Vector3(0.5f, 0f, 0f), Quaternion.identity);
        star3.AddComponent<GetConsumed>();
        star3.GetComponent<GetConsumed>().SetPlanted(growPlantableStars);
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
