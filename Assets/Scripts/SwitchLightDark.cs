using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLightDark : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if player presses space

        //play animation (white square sprite fades)
        //play sound
        //if player state is dark, swithc to light
        //if player state is light swithc to dark
        //cannot switch during combat
        //activate dark grid or light grid
        if (Input.GetKeyDown("space"))
        {
            print("space key was pressed");
        }
    }
}
