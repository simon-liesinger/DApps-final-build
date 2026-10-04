// this script is attached the bullet prefab

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_collision : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    public float speed = 20f;
    public bool isHoming = false;

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
            Vector2 direction = (mousePos - transform.position).normalized;
            rb.velocity = direction * speed;
            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotZ);

        }
    }

    void OnTriggerEnter2D(Collider2D other) // destroys bullet when colliding 
    {
        if (other.CompareTag("Player")) return; // doesn't hit player
        Destroy(gameObject);
    } 
}