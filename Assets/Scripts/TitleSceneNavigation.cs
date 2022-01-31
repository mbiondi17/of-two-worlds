using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneNavigation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string sceneName) {
        try {
            SceneManager.LoadScene(sceneName);
        }
        catch {
            UnityEngine.Debug.Log("can't load scene: " + sceneName);
        }
    }

    public void Exit() {
        Application.Quit();
    }
}
