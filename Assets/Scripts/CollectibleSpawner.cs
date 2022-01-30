using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour {

    private bool used = false;

    public bool isUsed() {
        return used;
    }

    public void setUsed() {
        used = true;
    }
}
