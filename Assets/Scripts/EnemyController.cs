using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    public float speed;
    public int damage = 4;
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

    void OnTriggerEnter2D(Collider2D other) {

        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null) {
            if(controller.health > 0) {
                controller.ChangeHealth(damage);
                Destroy(gameObject);
            }
        }
        if (other.gameObject.CompareTag("Finish")) {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
    // Start is called before the first frame update
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

    void removeMe() {
        objSpawn.BroadcastMessage("killEnemy", SpawnerID);
        Destroy(gameObject);
    }

    void setName(int sName) {
        SpawnerID = sName;
    }

    // Update is called once per frame
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

    void FixedUpdate() {
        /*Vector2 position = rigidbody2D.position;
        //float rotation = rigidbody2D.rotation;
        //float direction = rotation + Mathf.PI / 2;
        //direction += Random.Range(-Mathf.PI / 4, Mathf.PI / 4);

        if(vertical) {
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
        }

        //rigidbody2D.rotation = direction;
        rigidbody2D.transform.LookAt(player.transform);
        rigidbody2D.MovePosition(position);*/
        /*if (Vector3.Distance(rigidbody2D.transform.position, player.transform.position) > 1f) {
            MoveTowards(player.transform.position);
            RotateTowards(player.transform.position);
        }*/
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
