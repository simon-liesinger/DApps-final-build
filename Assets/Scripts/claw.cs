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
    public Transform heldPiece;
    private Transform heldPart;
    private Rigidbody2D heldRigidBody;
    public LayerMask allowedLayers;
    private Vector3 positionOffset;
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
                Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(1, 1), 0, allowedLayers);
                // Debug.Log(colliders.Length);
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
                    heldPiece = closestCollider.transform;
                    //find the offset so an individual piece is held rather than the center
                    positionOffset = Quaternion.Inverse(transform.rotation) * (heldPiece.position - transform.position);
                    heldRigidBody = heldPiece.GetComponent<Rigidbody2D>();
                    // heldRigidBody.bodyType = RigidbodyType2D.Kinematic;
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
                    heldRigidBody.bodyType = RigidbodyType2D.Dynamic;
                    heldPart = null;
                    heldPiece = null;
                    heldRigidBody = null;
                }
            }

            //apply the mouse tracking force to the claw and the held piece
            float speedFalloff = (targetPosition - transform.position).magnitude<2.0?(targetPosition - transform.position).magnitude/2:1;
            rb.velocity = (targetPosition - transform.position).normalized * speed * speedFalloff;
            clawRotationOffset = targetRotation - transform.rotation.eulerAngles.z;
            if (clawRotationOffset > 180)
            {
                clawRotationOffset -= 360;
            }
            if (clawRotationOffset < -180)
            {
                clawRotationOffset += 360;
            }
            float rotFalloff = (Mathf.Abs(clawRotationOffset)<10)?clawRotationOffset/10:1;
            rb.angularVelocity = rotationSpeed * Mathf.Sign(clawRotationOffset) * rotFalloff;
            if (heldPiece != null)
            {
                heldRigidBody.velocity = rb.velocity;
                heldRigidBody.angularVelocity = rb.angularVelocity;

                //draw the piece towards the claw
                Vector3 direction = (transform.position - heldPiece.position + transform.rotation * positionOffset).normalized;
                heldRigidBody.AddForce(direction * holdSpeed);
                //find the relative rotation of the held piece to the claw
                pieceAngleOffset = targetRotation - heldPiece.rotation.eulerAngles.z - holdAngleOffset + 90 * ((Input.GetButton("1")?1:0) - (Input.GetButton("e")?1:0));
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
        }
    }
}
