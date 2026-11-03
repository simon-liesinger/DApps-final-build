using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EnemyPlayerTrack : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    private Vector2 position;
    private movement targetPos;
    private Vector2 aimPos;
    public float aimAngle;
    public Transform firePoint;
    public GameObject enemyBulletPrefab;
    public float speed = 20f;
    public float shotCooldownTime = 3f;
    public float shotTimer = 0f;
    public bool canFire = false;

    // Start is called before the first frame update
    void Start()
    {
        position = rigidBody.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            movement target = collision.GetComponent<movement>();
            //Debug.Log(target.transform.position);
            Vector2 aimingTowards = target.transform.position;
            

            //Debug.DrawRay(transform.position, aimingTowards, Color.green, 5f);
            Debug.DrawLine(position, aimingTowards, Color.green, 5f);


            aimAngle = Mathf.Atan2(aimingTowards.y - position.y, aimingTowards.x - position.x) * Mathf.Rad2Deg;

            Debug.Log(aimAngle);

            transform.rotation = Quaternion.Euler(0f, 0f, aimAngle);


            if (canFire == false)
            {
                shotTimer += 1f * Time.deltaTime;

                if (shotTimer > shotCooldownTime)
                {
                    shotTimer = 0f;
                    canFire = true;
                }
            }

            if (canFire)
            {
                fireBullet();
                canFire = false;
            }
        }


    }

/*    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            aimAngle = 0f;
            shotTimer = 0;
        }
    }*/

    void fireBullet()
    {
       

        GameObject enemyBulletObj = Instantiate(enemyBulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D rb = enemyBulletObj.GetComponent<Rigidbody2D>();

        // rb.velocity = new Vector2(aimingTowards.y - position.y, aimingTowards.x - position.y);
        rb.velocity = new Vector2(Mathf.Sin(aimAngle)*speed, Mathf.Cos(aimAngle)*speed);

        EnemyBulletCollision bullet = enemyBulletObj.GetComponent<EnemyBulletCollision>();
    }



}
