using UnityEngine;

public class movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool canJump = true;

    void Start()
    {
        
    }
    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * 5.0f, rb.velocity.y);
        if (Input.GetAxis("Vertical") > 0 && Physics2D.Raycast(transform.position - new Vector3(0.0f, 0.5f, 0.0f), new Vector2(0.0f, -1.0f)).distance < 1.0f)
        {

            if (canJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, 10.0f);
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        canJump = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        canJump = false;
    }
}       
