using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    Animator anim;
    [SerializeField] int health = 100;
    AttackAI attackAI;
    FollowAI followAI;
    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;
    HedgehogAttackAI hedgehogAttackAI;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (GetComponent<AttackAI>() != null) attackAI = GetComponent<AttackAI>();
        if (GetComponent<HedgehogAttackAI>() != null) hedgehogAttackAI = GetComponent<HedgehogAttackAI>();
        followAI = GetComponent<FollowAI>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(int damage)
    {
        if (health - damage > 0)
        {
            health -= damage;
            StartCoroutine(FlashRed());
        }
        else
        {
            if (attackAI != null) attackAI.enabled = false;
            if (hedgehogAttackAI != null) hedgehogAttackAI.enabled = false;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.isKinematic = true;
            anim.SetBool("IsDead", true);
            followAI.enabled = false;
            boxCollider.enabled = false;
            gameObject.tag = "DeadEnemy";
        }
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
    public int GetHealth()
    {
        return health;
    }
}
