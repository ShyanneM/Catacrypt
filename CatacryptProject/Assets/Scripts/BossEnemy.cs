using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    AudioSource audioSource;
    public GameObject hitEffect;
    private Transform target;
    public float speed = 3.0f;
    Rigidbody2D rigidbody2D;
    public int health = 13;
    Animator animator;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("playerController").GetComponent<Transform>(); 

  
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.HealthChange(-1);
        }
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