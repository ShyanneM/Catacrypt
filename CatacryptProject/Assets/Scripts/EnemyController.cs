using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    AudioSource audioSource;
    public GameObject hitEffect;
    private Transform target;
    public float speed = 3.0f;
    new Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;
    public Renderer ObjectRender;
    bool despawnImmunity = true;

    private float distance;

    Animator animator;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {

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


        transform.position = Vector2.MoveTowards(this.transform.position, playerController.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        if (ObjectRender.isVisible)
        {
            despawnImmunity = false;
            if (despawnImmunity = false && !ObjectRender.isVisible)
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