using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{

    public string bubbleText = "";
    public GameObject charTextPrefab;
    public float textBubbleYOffset = 1.7f;
    private GameObject charText;

    // Start is called before the first frame update
    void Start()
    {
         }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        charText = Instantiate(charTextPrefab, gameObject.transform.position + new Vector3(0, textBubbleYOffset, 0), Quaternion.identity);
        displayText(bubbleText);
        Debug.Log("Entered interactable area.");
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        Destroy(charText);
    }

    private void displayText(string text) {
        TextMeshPro charTextMesh = charText.GetComponentInChildren<TextMeshPro>();
        charTextMesh.text = bubbleText;
    }
}
