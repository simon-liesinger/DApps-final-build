using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class claw : MonoBehaviour
{
    public bool active;
    public float speed;
    public float rotationSpeed;
    public float holdSpeed;
    public float holdRotationSpeed;
    public Rigidbody2D rb;
    private Vector3 targetPosition;
    private bool clawClosed;
    private float targetRotation;
    private float clawRotationOffset;
    private float pieceAngleOffset;
    private float holdAngleOffset;
    private Transform heldPiece;
    private Transform heldPart;
    private Rigidbody2D heldRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            //find mouse position
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;
            targetPosition = Camera.main.ScreenToWorldPoint(mousePos);
            
            //potentially open or close the claw
            if (Input.GetMouseButton(0) && !clawClosed)
            {
                clawClosed = true;
                // find something on the boxes layer within a 1x1 box around the claw
                Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(1, 1), 0, LayerMask.GetMask("Boxes"));
                if (colliders.Length > 0)
                {
                    //find the closest collider to the claw
                    Collider2D closestCollider = colliders[0];
                    float closestDistance = Vector3.Distance(transform.position, closestCollider.transform.position);
                    foreach (Collider2D collider in colliders)
                    {
                        float distance = Vector3.Distance(transform.position, collider.transform.position);
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestCollider = collider;
                        }
                    }
                    //grab the closest collider
                    heldPart = closestCollider.transform;
                    heldPiece = heldPart.parent;
                    heldRigidBody = heldPiece.GetComponent<Rigidbody2D>();
                    //find the relative rotation of the held piece to the claw
                    holdAngleOffset = targetRotation - heldPiece.rotation.eulerAngles.z;
                    //and round it to the nearest 90 degrees
                    holdAngleOffset = Mathf.Round(holdAngleOffset / 90) * 90;
                }
            } else if (!Input.GetMouseButton(0) && clawClosed)
            {
                clawClosed = false;
                if (heldPiece != null)
                {
                    heldPart = null;
                    heldPiece = null;
                    heldRigidBody = null;
                }
            }

            if (clawClosed)
            {
                //apply the mouse tracking force to the claw and the held piece
                rb.velocity = (targetPosition - transform.position).normalized * speed;
                clawRotationOffset = targetRotation - transform.rotation.eulerAngles.z;
                if (clawRotationOffset > 180)
                {
                    clawRotationOffset -= 360;
                }
                if (clawRotationOffset < -180)
                {
                    clawRotationOffset += 360;
                }
                rb.angularVelocity = rotationSpeed * clawRotationOffset / Mathf.Abs(clawRotationOffset);
                if (heldPiece != null)
                {
                    heldRigidBody.velocity = rb.velocity;
                    heldRigidBody.angularVelocity = rb.angularVelocity;

                    //draw the piece towards the claw
                    Vector3 direction = (transform.position - heldPiece.position).normalized;
                    heldRigidBody.AddForce(direction * holdSpeed);
                    //find the relative rotation of the held piece to the claw
                    pieceAngleOffset = targetRotation - heldPiece.rotation.eulerAngles.z - holdAngleOffset;
                    if (pieceAngleOffset > 180)
                    {
                        pieceAngleOffset -= 360;
                    }
                    if (pieceAngleOffset < -180)
                    {
                        pieceAngleOffset += 360;
                    }
                    heldRigidBody.angularVelocity = holdRotationSpeed * pieceAngleOffset / Mathf.Abs(pieceAngleOffset);
                }
            } else
            {
                //apply the mouse tracking force entirely to the claw
                rb.velocity = (targetPosition - transform.position).normalized * speed;
                clawRotationOffset = targetRotation - transform.rotation.eulerAngles.z;
                if (clawRotationOffset > 180)
                {
                    clawRotationOffset -= 360;
                }
                if (clawRotationOffset < -180)
                {
                    clawRotationOffset += 360;
                }
                rb.angularVelocity = rotationSpeed * clawRotationOffset / Mathf.Abs(clawRotationOffset);
            }
        }
    }
}
