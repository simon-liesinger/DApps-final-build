using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float speed = 20f;
    public float faceDirection = -1;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal != 0)
        {
            faceDirection = horizontal;
        }

        if (Input.GetButtonDown("Fire1")) // left ctrl and space to shoot
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
        Rigidbody2D rb = bulletObj.GetComponent<Rigidbody2D>();
        
        rb.velocity = new Vector2(faceDirection * speed, 0f);

    }
}