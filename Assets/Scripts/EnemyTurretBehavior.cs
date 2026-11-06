using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EnemyTurretBehavior : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    private Vector2 position;
    private movement targetPos;
    private Vector2 aimPos;
    public float aimAngle;
    public Transform firePoint;
    public GameObject enemyBulletPrefab;
    public float speed = 12f;
    public float shotCooldownTime = 3f;
    public float shotTimer = 0f;
    public bool canFire = false;
    public int hitsTaken = 0;
    public int hitsCanTake = 4;
    public Collider2D hurtBox;
    

    // Start is called before the first frame update
    void Start()
    {
        position = rigidBody.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            if (collision.IsTouching(hurtBox))
            {
                bullet_collision bullet = GetComponent<bullet_collision>();
                hitsTaken++;
                
                if (hitsTaken >= hitsCanTake)
                {
                    
                    Destroy(gameObject);
                }
                bullet.DestroyBullet();
            }
        }
    }

    private void OnDestroy() // this function can be used when other enemy types are destroyed
    {
        movement playerObj = GameObject.FindGameObjectWithTag("Player").GetComponent<movement>();
        playerObj.IncreaseESlain();
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


            aimAngle = Mathf.Atan2(position.y - aimingTowards.y, position.x - aimingTowards.x) * Mathf.Rad2Deg;

            //Debug.Log(aimAngle);

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


    void fireBullet()
    {
       

        GameObject enemyBulletObj = Instantiate(enemyBulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D rb = enemyBulletObj.GetComponent<Rigidbody2D>();

        // rb.velocity = new Vector2(aimingTowards.y - position.y, aimingTowards.x - position.y);
        rb.velocity = new Vector2(Mathf.Cos(aimAngle*Mathf.Deg2Rad)*-speed, Mathf.Sin(aimAngle*Mathf.Deg2Rad)*-speed);
        //rb.velocity = new Vector2(90 - aimAngle, aimAngle);

        EnemyBulletCollision bullet = enemyBulletObj.GetComponent<EnemyBulletCollision>();
    }



}
