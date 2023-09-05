using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public static event Action OnEnemyDeath;
    public static event Action OnEnemyDamaged;

    AIPath aiPath;
    AIDestinationSetter destinationSetter;
    AudioSource audioSource;
    Animator animator;
    protected internal GameObject player;
    Vector2 velocity;
    
    public SpriteRenderer sprite;
    public GameObject lootType;
    public int damage = 2;
    public int loot = 2;
    public AudioClip damageSound;
    public AudioClip deathSound;
    protected internal bool followingPlayer = false;

    public float Health
    {
        set
        {
            health = value;
            if(health <= 0)
            {
                Defeated();
            }
        }
        get
        {
            return health;
        }
    }
    public float health = 10;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        aiPath = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        destinationSetter.target = player.transform;


    }
    private void Update()
    {
        velocity = GetComponent<AIPath>().velocity;
        MoveAnimation();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log("Collision with " + other.gameObject.name);
        switch (other.gameObject.layer)
        {
            case 6:
                //  Player layer: hurt the player
                other.gameObject.GetComponent<PlayerStatus>().PlayerTakeDamage(damage);
                other.gameObject.GetComponent<KnockbackFeedback>().Knockback(this.gameObject);

                break;
            case 7:
                //  Enemies layer: collide, do nothing
                break;
            case 8:
                //  Projectiles layer: ignore for now
                break;
            default:
                break;
        }

    }

    public void Defeated()
    {
        animator.SetTrigger("Defeated");
        PlayDeathSound();
        RemoveEnemy();
        LootDrop();
        OnEnemyDeath?.Invoke();
    }
    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }
    public void LootDrop()
    {
        int lootValue = loot;
        GameObject lootPrefab = lootType;
        for (int i = 0; i < lootValue; i++)
        {
            GameObject loot = Instantiate(lootPrefab, this.transform.position, Quaternion.identity);

        }
    }
    public void EnemyTakeDamage(int damage)
    {
        Health -= damage;
        ChaseTarget();
        aiPath.canMove = true; followingPlayer = true;
        if (Health > 0)
        {
            PlayDamageSound();
        }
        StartCoroutine(EnemyFlashColor(Color.blue));
        OnEnemyDamaged?.Invoke();
    }

    IEnumerator EnemyFlashColor(Color color)
    {
        sprite.color = color;

        yield return new WaitForSeconds(0.1f);

        sprite.color = Color.white;

    }
    public void PlayDamageSound()
    {
            audioSource.PlayOneShot(damageSound, 0.8f);
    }
    public void PlayDeathSound()
    {
        GameObject sfxManager = GameObject.Find("SFXManager");
        AudioSource audio = sfxManager.GetComponent<AudioSource>();

        audio.PlayOneShot(deathSound, 0.8f);

    }


    virtual protected void ChaseTarget()
    {
        this.GetComponent<FlockerScript>().SwitchFlockingMode(FlockerScript.FlockingMode.ChaseTarget);
    }
    virtual protected void MoveAnimation()
    {
        /*
        if (velocity.x > 0 || velocity.y > 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        */
    }

}
