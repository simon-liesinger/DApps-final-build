using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float speed = 20f;
    public float faceDirection = -1; // shoots left by default 
    public bool canFire = true;
    private float timer;
    public float timeBetweenFiring;
    public bool homingActive = false;

    private Coroutine homingCoroutine;

    public void ActivateHoming(float duration)
    {
        if (homingCoroutine != null)
        {
            StopCoroutine(homingCoroutine);
        }

        homingCoroutine = StartCoroutine(HomingTimer(duration));
    }

    private System.Collections.IEnumerator HomingTimer(float duration)
    {
        homingActive = true;

        yield return new WaitForSeconds(duration);

        homingActive = false;
        homingCoroutine = null;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal != 0)
        {
            faceDirection = horizontal;
        }

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }             
        }

        if (Input.GetButtonDown("Fire1") && canFire) // left ctrl and space to shoot
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
        Rigidbody2D rb = bulletObj.GetComponent<Rigidbody2D>();
        
        rb.velocity = new Vector2(faceDirection * speed, 0f);

        bullet_collision bullet = bulletObj.GetComponent<bullet_collision>();

        if (bullet != null)
        {
            bullet.isHoming = homingActive;
        }

        canFire = false;
    }
}