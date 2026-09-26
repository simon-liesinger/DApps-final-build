// this script is attached the bullet prefab

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_collision : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 1f); // destroys bullet after 2 seconds
    }

    void OnTriggerEnter2D(Collider2D other) // destroys bullet when colliding 
    {
        if (other.CompareTag("Player")) return; 
        Destroy(gameObject);
    }
}