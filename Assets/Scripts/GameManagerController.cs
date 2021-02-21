using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public List<GameObject> spawners;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        PlayerController player = GetComponent<PlayerController>();
        SpawnController spawners = GetComponent<SpawnController>();
        if (player.health <= 0) {
            //EndGame();
        }
    }
/*
    void EndGame() {
        foreach (GameObject wall in spawners)
        {
            wall.waveSpawn = false;
        }
    }*/
}
