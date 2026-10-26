using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletCollision : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private float speed = 12f;

    void Start()
    {
        Destroy(gameObject, 3f);

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) return; // doesn't hit enemies
        Destroy(gameObject);
    }
}
