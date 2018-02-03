using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public float delayForStartSpawning;
    public float secondsForNewSpawn;
    public GameObject enemy1;
    

    void Start()
    {
        InvokeRepeating("Spawn", delayForStartSpawning, secondsForNewSpawn);
    }

    void Spawn()
    {
        Instantiate(enemy1, transform.position, transform.rotation);
    }
}
