using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStatus : MonoBehaviour
{
    Animator animator;
    public SpriteRenderer sprite;
    PlayerController playerController;

    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;
    public bool isInvincible = false;

    public float maxHealth = 12;
    
    public float Health
    {
        set
        {
            health = value;
            if (health <= 0)
            {
                Defeated();
            }
        }
        get
        {
            return health;
        }
    }
    public float health = 12;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    public void EnableInvinciblity()
    {
        isInvincible = true;
    }
    public void DisableInvinciblity() 
    {
        isInvincible = false;
    }
    public bool IsInvincible() 
    {
        return isInvincible;
     }
    public void PlayerTakeDamage(int damage)
    {
        if (!playerController.isInvincible)
        {
        Health -= damage;
        OnPlayerDamaged?.Invoke();
        StartCoroutine(PlayerFlashColor(Color.red));
        }
        else
        {
            ////
        }
    }

    public void Defeated()
    {
        animator.SetBool("isDefeated", true);
        OnPlayerDeath?.Invoke();
    }
    IEnumerator PlayerFlashColor(Color color)
    {
        sprite.color = color;

        yield return new WaitForSeconds(0.1f);

        sprite.color = Color.white;

    }
}
