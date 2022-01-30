using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{

    [TextArea]
    public string bubbleTextLightQuest = "";
    public string bubbleTextDarkQuest = "";
    public string bubbleTextNoQuest = "";

    public GameObject charTextPrefab;
    public GameObject collectiblePrefab;
    public float textBubbleYOffset = 1.7f;
    private GameObject charText;
    public bool givesQuest = true;
    private bool givenQuest = false;
    private GameObject collectibleSpawnerWrapper;

    private void OnTriggerEnter2D(Collider2D other) {
        //Always display text when getting near an interactable
        charText = Instantiate(charTextPrefab, gameObject.transform.position + new Vector3(0, textBubbleYOffset, 0), Quaternion.identity);
        if(!givesQuest) {
            displayText(bubbleTextNoQuest);
        } else {
            var isDarkWorld = GameObject.FindObjectOfType<GameManager>().GetState() == GameManager.States.darkWorld;
            displayText(isDarkWorld ? bubbleTextDarkQuest : bubbleTextLightQuest);
        }
        //If it gives a quest and has not given a quest
        if (givesQuest && !givenQuest) {
            //For now, just spawn a collectible
            spawnCollectible();
            this.givenQuest = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        Destroy(charText);
    }

    private void displayText(string text) {
        TextMeshPro charTextMesh = charText.GetComponentInChildren<TextMeshPro>();
        charTextMesh.text = text;
    }

    private void spawnCollectible(){
        collectibleSpawnerWrapper = GameObject.Find("CollectibleSpawners");
        Vector3 spawnLocation = collectibleSpawnerWrapper.GetComponent<CollectibleSpawnerWrapper>().getRandomSpawnerLocation();
        createCollectible(spawnLocation);
    }

    private void createCollectible(Vector3 spawnLocation){
        //Debug.Log("Creating a collectible at " + spawnLocation);
        GameObject newCollectible = Instantiate(collectiblePrefab, spawnLocation, Quaternion.identity);
    }
}
