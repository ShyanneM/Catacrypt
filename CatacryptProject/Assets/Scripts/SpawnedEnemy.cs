using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedEnemy : MonoBehaviour
{
    AudioSource audioSource;
    public GameObject hitEffect;
    private Transform target;
    public float speed = 3.0f;
    new Rigidbody2D rigidbody2D;
    public Renderer ObjectRender;
    private float distance;


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
        distance = Vector2.Distance(transform.position, playerController.transform.position);
        Vector2 direction = playerController.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        if (!ObjectRender.isVisible)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.HealthChange(-1);
            Destroy(gameObject);
        }
    }


    public void Contact()
    {
        Destroy(gameObject);
    }
}
