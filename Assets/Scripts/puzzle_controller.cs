using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzle_controller : MonoBehaviour
{
    private CompositeCollider2D outOfBounds;
    private List<GameObject> pieces = new List<GameObject>();
    private List<Transform>[] extraGoals = new List<Transform>[4];
    public bool currentlyCompleted;
    public bool currentlyOptCompleted;
    private int minCount;
    private int maxCount;
    // Start is called before the first frame update
    void Start()
    {
        //find all the objects
        outOfBounds = transform.gameObject.GetComponent<CompositeCollider2D>();
        foreach (Transform child in transform.Find("Pieces"))
        {
            pieces.Add(child.gameObject);
        }
        foreach (Transform child in transform.Find("ExtraGoals").Find("High"))
        {
            extraGoals[0].Add(child);
        }
        foreach (Transform child in transform.Find("ExtraGoals").Find("Low"))
        {
            extraGoals[1].Add(child);
        }
        foreach (Transform child in transform.Find("ExtraGoals").Find("OptHigh"))
        {
            extraGoals[2].Add(child);
        }
        foreach (Transform child in transform.Find("ExtraGoals").Find("OptLow"))
        {
            extraGoals[3].Add(child);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentlyCompleted = true;
        currentlyOptCompleted = true;
        foreach (GameObject piece in pieces)
        {
            if (outOfBounds.IsTouching(piece.GetComponent<CompositeCollider2D>()))
            {
                currentlyCompleted = false;
            }
        }
        (minCount, maxCount) = scanGoals(extraGoals[0]);
        if (minCount < 6)
        {
            currentlyCompleted = false;
        }
        (minCount, maxCount) = scanGoals(extraGoals[1]);
        if (minCount > 2)
        {
            currentlyCompleted = false;
        }
        (minCount, maxCount) = scanGoals(extraGoals[2]);
        if (minCount < 6)
        {
            currentlyOptCompleted = false;
        }
        (minCount, maxCount) = scanGoals(extraGoals[3]);
        if (minCount > 2)
        {
            currentlyOptCompleted = false;
        }
    }
    (int, int) scanGoals(List<Transform> positions)
    {
        // Scan the goals and return the minimum and maximum counts of nearby game objects on the boxes layer
        int minCount = int.MaxValue;
        int maxCount = int.MinValue;
        foreach (Transform pos in positions)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(pos.position, new Vector2(1, 1), 0f, LayerMask.GetMask("Boxes"));
            int count = colliders.Length;
            if (count < minCount)
            {
                minCount = count;
            }
            if (count > maxCount)
            {
                maxCount = count;
            }
        }
        return (minCount, maxCount);
    }
}
