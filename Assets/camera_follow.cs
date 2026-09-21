using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow : MonoBehaviour
{
    public Transform follow;
    public Vector3 offset;
    private Vector3 lastPosition;
    bool caught;
    private Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        caught = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - (follow.position + offset)).magnitude > 5.0f)
        {
            caught = false;
        } else if ((transform.position - (follow.position + offset)).magnitude < 1.0f) {
            caught = true;
        }
        if (caught)
        {
            transform.position = follow.position + offset;
        } else
        {
            Debug.Log(0.01f*Time.deltaTime);
            target = follow.position+(10*follow.position-10*lastPosition) + offset;
            transform.position = Vector3.Lerp(transform.position, target, 0.02f);
        }
        lastPosition = follow.position;
    }
}
