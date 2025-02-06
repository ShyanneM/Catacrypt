using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    AudioSource audioSource;
    public AudioClip throwClip1;
    public AudioClip throwClip2;



    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
        float fireSound = Random.Range(1,2);
        if ((int) fireSound == 1)
        {
            audioSource.PlayOneShot(throwClip1);
        }
        else
            audioSource.PlayOneShot(throwClip2);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController e = other.collider.GetComponent<EnemyController>();
        Spawner s = other.collider.GetComponent<Spawner>(); 

        if (e != null || s != null )
        {
            e.Contact();
            s.Contact();
        }

        // Adding the ToughEnemy to get registered to getting ht
        BossEnemy f = other.collider.GetComponent<BossEnemy>();
        if (f != null)
        {
            f.TakeDamage(1);
        }

        BigSpawner b = other.collider.GetComponent<BigSpawner>();
        if (b != null)
        {
            b.TakeDamage(1);
        }


        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }
}