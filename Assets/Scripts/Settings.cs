using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    private static Settings instance;
    public static Settings Instance { get { return instance; } }
    
    public float normalTowerDespawnDelay;

    private void Awake() {
        if(instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
    }
}
