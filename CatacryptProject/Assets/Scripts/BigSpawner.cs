using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSpawner : MonoBehaviour
{
    public float spawnGap = 1.0f;
    public float timeBeforeSpawn;
    public Renderer ObjectRender;
    public GameObject basicEnemy;
    Rigidbody2D rigidbody2D;
    public int health = 3;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        ObjectRender = GetComponent<Renderer>();

    }

    void Update()
    {
        if (ObjectRender.isVisible)
        {

            timeBeforeSpawn += Time.deltaTime;
            if (timeBeforeSpawn == spawnGap)
            {
                GameObject spawnedEnemy = Instantiate(basicEnemy, rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);
                timeBeforeSpawn = 0.0f;
            }
        }
    }

    public void Contact()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health < 1)
        {
            Destroy(gameObject);
        }
        //code for flashing upon damage
    }

}