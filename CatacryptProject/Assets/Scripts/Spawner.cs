using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner: MonoBehaviour
{
    public float spawnGap = 1.0f;
    public float timeBeforeSpawn;
    public Renderer ObjectRender;
    public GameObject basicEnemy;
    new Rigidbody2D rigidbody2D;
   

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

}