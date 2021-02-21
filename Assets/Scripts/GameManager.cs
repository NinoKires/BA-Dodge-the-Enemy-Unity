using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<SpawnController> spawners;
    public PlayerController player;
    private int playerHealth;
    public GameObject controller;
    public Timer timer;
    public GameObject gameOverText;
    public UiManagerController uiManager;
    private bool tempBool = true;

    // Start is called before the first frame update
    void Start()
    {
        //controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth = player.health;

        if(playerHealth <= 0) {
            //Debug.Log("start sequence");
            StartCoroutine( GameOverSequence());
        }
    }
    IEnumerator GameOverSequence() {
        while(tempBool) {
            //Debug.Log("enter sequence");
            timer.timerActive = false;
            Destroy(player);
            EndSpawn();
            //Debug.Log("first wait");
            yield return new WaitForSeconds(1.0f);
            Destroy(controller);
            gameOverText.SetActive(true);
            //Debug.Log("second wait");
            yield return new WaitForSeconds(2.0f);
            uiManager.Menu();
            yield return null; 
        }
    }

    void EndSpawn() {
        for (int i = 0; i <= spawners.Count-1; i++) {
            spawners[i].Spawn = false;
        }
    }
}
