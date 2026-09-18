using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y-1);
        transform.position = new Vector3(transform.position.x+Input.GetAxis("Horizontal"), transform.position.y, transform.position.z);
    }
}
