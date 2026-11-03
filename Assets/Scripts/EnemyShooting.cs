/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject enemyBulletPrefab;
    public float speed = 20f;
    public float shotCooldownTime = 3f;
    public float shotTimer = 0f;
    public bool canFire = false;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerStay2D(Collider other)
    {
        if (canFire == false)
        {
            shotTimer += 1f * Time.deltaTime;

            if (shotTimer > shotCooldownTime)
            {
                canFire = true;
                shotTimer = 0f;
            }
        }

        if (canFire)
        {
            fireBullet();
            canFire = false;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

  *//*  void fireBullet()
    {
        GameObject enemyBulletObj = Instantiate(enemyBulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D rb = enemyBulletObj.GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(Mathf.Cos(aimAngle), Mathf.Sin(aimAngle));

        EnemyBulletCollision bullet = enemyBulletObj.GetComponent<EnemyBulletCollision>();
    }
}*/
