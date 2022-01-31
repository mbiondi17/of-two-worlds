using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region NPC Spawn Info
    public GameObject NPCSpawnCenter;
    public List<Vector3> npcSpawnLocations;
    public List<GameObject> npcPrefabs;
    public List<Sprite> collectiblesRetrieved;
    public List<Sprite> collectiblesReturned;

    [Range(3,6)]
    public int numQuests = 3;
    #endregion

    #region Enemy Spawn Info
    public GameObject enemyPrefab;
    public float spawnTime = 0.6f;
    float lastSpawn = 0.0f;
    public GameObject enemySpawnCenter;
    public List<Vector3> enemySpawnLocations;
    #endregion

    #region Combat Info
    float lastCombatStart = 0.0f;
    public float combatLength = 30f;
    private Collectible currentCollectible;
    #endregion

    #region State Management
    public enum States { lightWorld, darkWorldCombat, darkWorld };

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

    float endofGameTimer = -1.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawnLocations.Clear();
        SpawnNPCs();
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
                HandleCombatEnd(true);
            }
        }
        if(collectiblesReturned.Count == numQuests) {
            GameObject.Find("WinText").GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            if(endofGameTimer < 0) {
                endofGameTimer = Time.time + 3;
            }
        }
        if(endofGameTimer > 0.0) {
            if(Time.time > endofGameTimer) {
                SceneManager.LoadScene("Credits");
            }
        }
    }

    public void SetCollectible(Collectible collectible) {
        this.currentCollectible = collectible;
    }
    public void StartCombat() {
        enemySpawnLocations.Clear();
        var playerPosition = FindObjectOfType<PlayerController>().GetComponent<Transform>().position;
        this.enemySpawnCenter.transform.position = playerPosition;
        this.lastCombatStart = Time.time;
        foreach(Transform child in enemySpawnCenter.transform) {
            enemySpawnLocations.Add(child.transform.position);
        }
        lastSpawn = Time.time;
    }

    void SpawnEnemy() {
        if(enemySpawnLocations != null) {
            int spawnIndex = Random.Range(0,enemySpawnLocations.Count);
            Vector3 spawnLoc = enemySpawnLocations[spawnIndex];
            var enemyObj = Instantiate(enemyPrefab, spawnLoc, Quaternion.identity);
        }
    }

    public void PlayerDied() {
        HandleCombatEnd(false);
    }

    void HandleCombatEnd(bool victory) {
        var enemies = FindObjectsOfType<EnemyController>();
        foreach(var enemy in enemies) {
            GameObject.Destroy(enemy.gameObject);
        }
        this.SetState(States.lightWorld);
        if(victory) {
            this.currentCollectible.OnPlayerGet();
            this.collectiblesRetrieved.Add(currentCollectible.GetComponent<SpriteRenderer>().sprite);
        }
    }

    private void SpawnNPCs()
    {
        foreach(Transform spawn in NPCSpawnCenter.transform) {
            npcSpawnLocations.Add(spawn.transform.position);
        }

        List<int> spawnIndices = new List<int>();
        //breaks if more prefabs than spawn locations!
        while(spawnIndices.Count < npcPrefabs.Count) {
            var nextLoc = Random.Range(0, npcSpawnLocations.Count);
            if(spawnIndices.Contains(nextLoc)) continue;
            else spawnIndices.Add(nextLoc);
        }

        List<int> questGivers = new List<int>();
        //breaks if more quests than prefabs! (currently quest # limited in editor)
        while(questGivers.Count < numQuests) {
            var nextLoc = Random.Range(0, npcPrefabs.Count);
            if(questGivers.Contains(nextLoc)) continue;
            else questGivers.Add(nextLoc);
        }

        for(int i = 0; i < npcPrefabs.Count; i++) {
            //
            var newNPC = Instantiate(npcPrefabs[i], npcSpawnLocations[spawnIndices[i]], Quaternion.identity);
            newNPC.GetComponent<Interactable>().givesQuest = questGivers.Contains(i);
        }

    }
}
