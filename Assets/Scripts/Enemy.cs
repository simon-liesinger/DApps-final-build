using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public int maxHealth = 3;
    public float moveSpeed = 5f;
    public float detectRange = 6f;
    public float burstRange = 10f;
    public int burstDamage = 1;
    public float jumpSpeed = 8f;
    public float wallCheckDistance = 0.3f;
    public LayerMask groundLayer;
    private Collider2D body;

    protected int health;
    protected Transform player;
    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();

        foreach (BoxCollider2D c in GetComponents<BoxCollider2D>())
        {
            if (!c.isTrigger)
            {
                body = c;
                break;
            }
        }

        GameObject p = GameObject.FindWithTag("Player");
        if (p != null) player = p.transform;
    }

    protected virtual void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= burstRange)
            Burst();
        else if (distance <= detectRange)
            Chase();
        else
            rb.velocity = new Vector2(0f, rb.velocity.y);
    }

    protected virtual void Chase()
    {
        float direction = Mathf.Sign(player.position.x - transform.position.x);
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);

        if (body != null && IsGrounded() && WallAhead(direction))
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
    }

    private bool IsGrounded()
    {
        Bounds b = body.bounds;
        return Physics2D.BoxCast(b.center, new Vector2(b.size.x * 0.9f, b.size.y),
                                 0f, Vector2.down, 0.1f, groundLayer);
    }

    private bool WallAhead(float direction)
    {
        Bounds b = body.bounds;
        Vector2 origin = new Vector2(b.center.x, b.min.y + 0.1f);
        return Physics2D.Raycast(origin, Vector2.right * direction,
                                 b.extents.x + wallCheckDistance, groundLayer);
    }

    protected virtual void Burst()
    {
        IDamageable target = player.GetComponent<IDamageable>();
        Debug.Log(name + " burst | player damageable found: " + (target != null));

        if (target != null)
            target.TakeDamage(burstDamage);

        Destroy(gameObject);
    }

    public virtual void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
            Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, burstRange);
    }
}