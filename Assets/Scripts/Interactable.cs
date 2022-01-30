using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    public AudioSource thinkingsound;

    [TextArea]
    public string bubbleTextLightQuest = "";
    [TextArea]
    public string bubbleTextDarkQuest = "";
    [TextArea]
    public string bubbleTextNoQuest = "";
    [TextArea]
    public string bubbleTextQuestComplete = "";

    public Sprite collectibleSprite;
    public GameObject charTextPrefab;
    public GameObject collectiblePrefab;
    public float textBubbleYOffset = 1.7f;
    private GameObject charText;
    public bool givesQuest = true;
    private bool givenQuest = false;
    private bool questComplete = false;
    public string tutorialType = "";
    private GameObject collectibleSpawnerWrapper;

    private void Start()
    {
        thinkingsound = GetComponent<AudioSource>();
    }

        private void OnTriggerEnter2D(Collider2D other) {

        var isDarkWorld = GameObject.FindObjectOfType<GameManager>().GetState() == GameManager.States.darkWorld;
        //Always display text when getting near an interactable
        charText = Instantiate(charTextPrefab, gameObject.transform.position + new Vector3(0, textBubbleYOffset, 0), Quaternion.identity);
        if(!givesQuest) {
            displayText(bubbleTextNoQuest);
        } else if(questComplete) { 
            displayText(bubbleTextQuestComplete);
        } else {
            displayText(isDarkWorld ? bubbleTextDarkQuest : bubbleTextLightQuest);
        }
        //If it gives a quest and has not given a quest
        if (givesQuest && !givenQuest) {
            //For now, just spawn a collectible
            spawnCollectible();
            this.givenQuest = true;
        }
        var gm = FindObjectOfType<GameManager>();
        if(givenQuest && gm.collectiblesRetrieved.Contains(this.collectibleSprite)) {
            questComplete = true;
            displayText(bubbleTextQuestComplete);
            gm.collectiblesRetrieved.Remove(this.collectibleSprite);
            gm.collectiblesReturned.Add(this.collectibleSprite);
        }

        //Tutorial NPCs
        if (tutorialType != "") {
            displayText(isDarkWorld ? bubbleTextDarkQuest : bubbleTextLightQuest);
            displayTutorialImage(tutorialType);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        Destroy(charText);
        GameObject tutorialImage = GameObject.Find("TutorialImage");
        tutorialImage.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void displayText(string text) {
        TextMeshPro charTextMesh = charText.GetComponentInChildren<TextMeshPro>();
        charTextMesh.text = text;
        //play sound
        if (!thinkingsound.isPlaying)
        {
            thinkingsound.Play();
        }
    }

    private void displayTutorialImage(string type) {
        GameObject tutorialImage = GameObject.Find("TutorialImage");
        tutorialImage.GetComponent<SpriteRenderer>().enabled = true;
        tutorialImage.transform.position = charText.transform.position;
    }

    private void spawnCollectible(){
        collectibleSpawnerWrapper = GameObject.Find("CollectibleSpawners");
        Vector3 spawnLocation = collectibleSpawnerWrapper.GetComponent<CollectibleSpawnerWrapper>().getRandomSpawnerLocation();
        createCollectible(spawnLocation);
    }

    private void createCollectible(Vector3 spawnLocation){
        //Debug.Log("Creating a collectible at " + spawnLocation);
        GameObject newCollectible = Instantiate(collectiblePrefab, spawnLocation, Quaternion.identity);
        newCollectible.GetComponent<SpriteRenderer>().sprite = this.collectibleSprite;
    }
}
