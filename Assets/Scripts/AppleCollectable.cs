﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleCollectable : MonoBehaviour
{
    public int healing = 3;
    public float minSpeed;
    public float maxSpeed;
    public float speed;
    public bool vertical;
    Animator animator;
    public float changeTime = 3.0f;
    float timer;
    int direction = 1;
    Rigidbody2D rigidbody2D;
    //---------
    private GameObject objSpawn;
    private int SpawnerID;
    private GameObject player;
    private Vector2 movementDirection;
    private Vector2 mps;

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();
        int temp = -1*healing;
        if (controller != null) {
            controller.ChangeHealth( temp );
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Finish")) {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0) {
            direction = -direction;
            timer = changeTime;
        }

        rigidbody2D.transform.position = new Vector2(rigidbody2D.transform.position.x + (mps.x * Time.deltaTime),
        rigidbody2D.transform.position.y + (mps.y * Time.deltaTime));
    }

    void Start()
    {
        player = GameObject.Find("Player");
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>(); 
        //---
        objSpawn = (GameObject) GameObject.FindWithTag("EnemySpawner");

        speed = Random.Range(minSpeed, maxSpeed);

        calcDirection();

        //MoveTowards(player.transform.position);
        RotateTowards(movementDirection);//RotateTowards(player.transform.position);
    }

    void calcDirection() {
        movementDirection = new Vector2(Random.Range(-4.0f, 4.0f), Random.Range(-4.0f, 4.0f)).normalized; //Random.Range(-4.0f, 4.0f)
        mps = movementDirection * speed;
    }

    private void MoveTowards(Vector2 target) {
        rigidbody2D.transform.position = Vector2.MoveTowards(rigidbody2D.transform.position, target, speed * Time.deltaTime);
    }

    private void RotateTowards (Vector2 target) {
        var offset = 135f; //90f;
        Vector2 direction = target - (Vector2)rigidbody2D.transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rigidbody2D.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }
}
