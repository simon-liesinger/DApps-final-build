using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_collision : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float speed = 20f;
    public bool isHoming = false;
    public int damage = 1;
    private bool hasHit;


    private void Start()
    {
        Destroy(gameObject, 1f); // destroys bullet after 1 second

        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(mainCam.transform.position.z - transform.position.z))); 
        // gets mouse position

        if(isHoming)
        {
            damage = 10;
            Vector2 direction = (mousePos - transform.position).normalized;
            rb.velocity = direction * speed;
            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (hasHit) return;
        if (other.CompareTag("Player")) return;
        if (other.CompareTag("Enemy") && other.isTrigger) return;
        if (other.CompareTag("EnemyHurtBox")) return;

        hasHit = true;

        IDamageable target = other.GetComponentInParent<IDamageable>();
        if (target != null)
            target.TakeDamage(damage);

        Destroy(gameObject);
    }

    public void DestroyBullet()
    {
        DestroyBullet();
    }
}