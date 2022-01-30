using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum States { lightWorld, darkWorldCombat, darkWorld };
    public GameObject enemyPrefab;

    #region Spawn Info
    public float spawnTime = 0.6f;
    float lastSpawn = 0.0f;
    public GameObject enemySpawnCenter;
    public List<Vector3> spawnLocations;
    #endregion

    #region Combat Info
    float lastCombatStart = 0.0f;
    public float combatLength = 30f;
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
            if(Time.time >= lastCombatStart + combatLength) {
            HandleCombatEnd();
        }
        }
    }

    public void StartCombat() {
        spawnLocations.Clear();
        var playerPosition = FindObjectOfType<PlayerController>().GetComponent<Transform>().position;
        this.enemySpawnCenter.transform.position = playerPosition;
        this.lastCombatStart = Time.time;
        foreach(Transform child in enemySpawnCenter.transform) {
            spawnLocations.Add(child.transform.position);
        }
        lastSpawn = Time.time;
    }

    void SpawnEnemy() {
        if(spawnLocations != null) {
            int spawnIndex = Random.Range(0,spawnLocations.Count);
            Vector3 spawnLoc = spawnLocations[spawnIndex];
            var enemyObj = Instantiate(enemyPrefab, spawnLoc, Quaternion.identity);
        }
    }

    void HandleCombatEnd() {
        var enemies = FindObjectsOfType<EnemyController>();
        foreach(var enemy in enemies) {
            GameObject.Destroy(enemy.gameObject);
        }
        currentState = States.lightWorld;
        //TODO : trigger light world flip here
    }
}
