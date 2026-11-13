using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Behavior : MonoBehaviour
{
    public int hitsTaken = 0;
    private int hitsCanTake = 4;
    public BoxCollider2D hurtBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // collision for the hurt box
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            if (collision.IsTouching(hurtBox))
            {
                hitsTaken++;
                if (hitsTaken >= hitsCanTake)
                {
                    Destroy(gameObject);
                    
                }
            }
        }
    }
}
