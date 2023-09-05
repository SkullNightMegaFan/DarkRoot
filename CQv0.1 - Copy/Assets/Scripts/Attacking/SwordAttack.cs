using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public CapsuleCollider2D swordCollider;
    public int damage = 10;
    public ParticleSystem swordEffect;

    public GameObject player;

    public void Attack()
    {
        swordEffect.Play();
        swordCollider.enabled = true;
        StartCoroutine(StopAfterWait());
        
    }
        public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    IEnumerator StopAfterWait()
    {
        yield return new WaitForSeconds(0.1f);
        StopAttack();
    }

        private void OnTriggerEnter2D(Collider2D other)
    { // check if the hitbox has collided with an enemy, if so, do damage to it
        if(other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();  //grab the enemy script attached to the collided enemy 
            KnockbackFeedback knockback = other.GetComponent<KnockbackFeedback>();

            if(enemy != null)
            {
                enemy.EnemyTakeDamage(damage);

                if (knockback != null)
                {
                    knockback.Knockback(player);
                }
                StopAttack();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        StopAttack();
    }



    public void FailedAttack()
    {
        swordEffect.Play();

    }
}
