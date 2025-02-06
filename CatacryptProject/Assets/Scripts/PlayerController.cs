using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;
using System;

public class PlayerController : MonoBehaviour
{
    AudioSource audioSource;

    //character list
    public string[] characters = {"Knight", "Fae", "Witch", "Nephilim"};    
    public string selectedCharacter;

    public GameObject knight;
    public GameObject fae;
    public GameObject witch;
    public GameObject nephilim;

    public GameObject knightClone;

    //stats
    public string moveSpeed;
    public string damageTaken;
    public float fireRate;
    public int projectileRange;

    public int score = 0;
    public bool gameOver = false;

    public float health;
    public GameObject projectilePrefab;
    public int maxHealth = 800;

    public float canFireTimer;
    public float speed;
    public float timeInvincible;
    

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    bool canFire = true;
    public bool hasKey = false;
    

    bool isInvincible;
    float invincibleTimer = 0.4f;

    bool hasRoomClearItem;
    public GameObject roomClearItem;
    //variables to control win/loss and game over
    public string winLoss;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    public TextMeshProUGUI healthText;




    // Start is called before the first frame update
    void Start()
    {
        isInvincible = false;
        float canFireTimer = fireRate;
        hasRoomClearItem = false;
        health = maxHealth;
        rigidbody2d = GetComponent<Rigidbody2D>();
        Vector2 startPosition = new Vector2(-4f, 4f);
        
        switch (selectedCharacter) //character stat assigning upon the game beginning
        {
            case "Knight":
                moveSpeed = "slow";
                damageTaken = "low";
                fireRate = 1.00f;
                projectileRange = 100;
                //GameObject knightClone = Instantiate(knight, startPosition);
                break;
            case "Fae":
                moveSpeed = "fast";
                damageTaken = "high";
                fireRate = 0.5f;
                projectileRange = 200;
                //instantiate game object for character :3
                break;
            case "Witch":
                moveSpeed = "mid";
                damageTaken = "middle";
                fireRate = 1.25f;
                projectileRange = 150;
                //instantiate game object for character :3
                break;
            case "Nephilim":
                moveSpeed = "mid";
                damageTaken = "middle";
                fireRate = 1.75f;
                projectileRange = 150;
                //instantiate game object for character :3
                break;
        }
        switch (moveSpeed)
        {
            case "mid":
                speed = 2.0f;
                break;
            case "slow":
                speed = 1.5f;
                break;
            case "fast":
                speed = 2.5f;
                break;
        }
        
        if (healthText != null)
        {
            healthText.text = "" + health;
        }
        else
        {
            Debug.LogWarning("HealthText is not assigned in the inspector.");
        } 


    }

    private void Instantiate(GameObject knight, Vector2 startPosition)
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);


        if ((int)health < 1)
        {
            winLoss = "Loss";
            FinishGame(1);
        }

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (canFire)
            {
                Launch(projectileRange);
                canFire = false;
                canFireTimer -= Time.deltaTime;
                if (canFireTimer == 0)
                    canFire = true;
            }

        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("Game ended.");
            Application.Quit();
        }
        
    }

    void FixedUpdate()
    {
        //project settings > input has horizontal and vertical axis assigned to wasd/arrow keys
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);

        health -= Time.deltaTime;
    }


    public void HealthChange(int amount)
    {
        
        if (amount < 0)
        {
            if (isInvincible)
                return;
            else
            {
                isInvincible = true;
                while (invincibleTimer > 0)
                    invincibleTimer -= Time.deltaTime;
                isInvincible = false;
                invincibleTimer = 0.4f;
            }

            switch (damageTaken)
            {
                case "low":
                    health -= UnityEngine.Random.Range(3,6);
                    break;
                case "high":
                    health -= UnityEngine.Random.Range(5,8);
                    break;
                case "middle":
                    health -= UnityEngine.Random.Range(4,7);
                    break;
            }

        }

        if (amount > 0)
        {
            //play sound
            health += 50;
        }
    }
    //key code pasted into player controller
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            hasKey = true;
            Destroy(other.gameObject);
            if (hasKey && Input.GetKeyDown(KeyCode.W))
            {
                TryUnlockDoor();
            }

        if (other.CompareTag("Room Clear"))
        {
            hasRoomClearItem = true;
            Destroy(other.gameObject);
            if (hasRoomClearItem && Input.GetKeyDown(KeyCode.Z))
            {
                EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
                SpawnedEnemy spawnedEnemy = other.gameObject.GetComponent<SpawnedEnemy>();
                BossEnemy bossEnemy = other.gameObject.GetComponent<BossEnemy>();  

                if (enemy != null)
                {
                    Destroy(enemy);
                }
                if (spawnedEnemy != null)
                {
                    Destroy(spawnedEnemy);
                }
                if (bossEnemy != null)
                {
                    bossEnemy.TakeDamage(4);
                }
                
            }

        }
        }
    }
    void TryUnlockDoor()
    {
        if (hasKey)
        {
            GameObject door = GameObject.FindWithTag("Door");
            if (door)
            {
                Destroy(door);
                hasKey = false;
            }
        }
    }

    void Launch(int projectileRange)
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, UnityEngine.Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, projectileRange);

        animator.SetTrigger("Launch");
    }

    void FinishGame(int numberOf)
    {
        if (numberOf == 0)
        {
            winLoss = "Win";
        }

        if (winLoss == "Win")
        {
            SceneManager.LoadScene("Victory Screen");
        }
        else if (winLoss == "Loss")
        {
            SceneManager.LoadScene("Game Over Screen");
        }
    }

    public void MenuSelectResult(int selection) //simulates the character select result
    {
        selectedCharacter = characters[selection];
        
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}

