using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homing_powerup : MonoBehaviour
{
    public float duration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shooting playerShooting = other.GetComponent<shooting>();

            if (playerShooting != null) 
            
            {
                playerShooting.ActivateHoming(duration);
                Destroy(gameObject);
            }            
        }        
    }
}
