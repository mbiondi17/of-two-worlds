using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum States { lightWorld, darkWorldCombat, darkWorld };
    public GameObject enemyPrefab;

    #region Spawn Info
    public float spawnTime = 0.6f;
    public float lastSpawn = 0.0f;
    public GameObject enemySpawnCenter;
    public List<Vector3> spawnLocations;
    #endregion

    #region State Management
    private States currentState;

    public void SetState(States state)
    {
        currentState = state;
        if(currentState == States.darkWorldCombat) StartCombat();
    }

    public States GetState() {
        return this.currentState;
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        spawnLocations.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == States.darkWorldCombat) {
            if(Time.time > lastSpawn + spawnTime) {
                SpawnEnemy();
                lastSpawn = Time.time;
            }
        }
    }

    public void StartCombat() {
        spawnLocations.Clear();
        var playerPosition = FindObjectOfType<PlayerController>().GetComponent<Transform>().position;
        this.enemySpawnCenter.transform.position = playerPosition;
        
        foreach(Transform child in enemySpawnCenter.transform) {
            spawnLocations.Add(child.transform.position);
        }
        lastSpawn = Time.time;
    }

    void SpawnEnemy() {
        int spawnIndex = Random.Range(0,spawnLocations.Count);
        Vector3 spawnLoc = spawnLocations[spawnIndex];
        var enemyObj = Instantiate(enemyPrefab, spawnLoc, Quaternion.identity);
    }
}
