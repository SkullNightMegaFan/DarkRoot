using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KnockbackFeedback : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    private Rigidbody2D rigidBody;

    [SerializeField]
    private float strength = 2f, delay = 0.2f;
    bool isPlayerSender = false;

    public UnityEvent OnBegin, OnDone;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Knockback(GameObject sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        if (sender.CompareTag("Player"))
        {
            isPlayerSender = true;
            animator.SetBool("isKnockback", true);
        }
        rigidBody.AddForce(direction * strength, ForceMode2D.Impulse);
        StartCoroutine(ResetKnockback(isPlayerSender));
    }

    private IEnumerator ResetKnockback(bool isPlayer)
    {
        yield return new WaitForSeconds(delay);
        rigidBody.velocity = Vector3.zero;
        if (isPlayer)
        {
            isPlayerSender = false;
            animator.SetBool("isKnockback", false);
        }
        OnDone?.Invoke();
    }

}
