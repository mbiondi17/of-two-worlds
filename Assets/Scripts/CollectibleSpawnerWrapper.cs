using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawnerWrapper : MonoBehaviour
{

    private List<GameObject> collectibleSpawnerList;

    public Vector3 getRandomSpawnerLocation(){
        //Recreates the list of valid spawners
        collectibleSpawnerList = new List<GameObject>();
        //Finds spawners by the tag, then adds them to the list if not used.
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("CollectibleSpawner")) {
            if (!obj.GetComponent<CollectibleSpawner>().isUsed()) {
                collectibleSpawnerList.Add(obj);
            }
        }
        //Gets the spawn index randomly
        int spawnIndex = Random.Range(0,collectibleSpawnerList.Count);
        //Sets the spawner of the given index to used so the same location isn't used twice
        collectibleSpawnerList[spawnIndex].GetComponent<CollectibleSpawner>().setUsed();
        //Debug.Log(collectibleSpawnerList.Count + " - " + spawnIndex);
        //Returns the Vector3 of the spawner's location
        return collectibleSpawnerList[spawnIndex].transform.position;
    }
}
