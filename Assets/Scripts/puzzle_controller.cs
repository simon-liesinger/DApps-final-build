using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using UnityEngine;

public class puzzle_controller : MonoBehaviour
{
    private CompositeCollider2D outOfBounds;
    private List<GameObject> pieces;
    private List<Transform>[4] extraGoals;
    public bool currentlyCompleted;
    public bool currentlyOptCompleted;
    private int minCount;
    private int maxCount;
    // Start is called before the first frame update
    void Start()
    {
        //find all the objects
        outOfBounds = transform.FindAllComponentsOfType<BoxCollider2D>();
        foreach (Transform child in transform.Find("parts"))
        {
            pieces.Add(child.gameObject);
        }
        foreach (Transform child in transform.Find("extraGoals").Find("High"))
        {
            extraGoals[0].Add(child.gameObject);
        }
        foreach (Transform child in transform.Find("extraGoals").Find("Low"))
        {
            extraGoals[1].Add(child.gameObject);
        }
        foreach (Transform child in transform.Find("extraGoals").Find("OptHigh"))
        {
            extraGoals[2].Add(child.gameObject);
        }
        foreach (Transform child in transform.Find("extraGoals").Find("OptLow"))
        {
            extraGoals[3].Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentlyCompleted = true;
        currentlyOptCompleted = true;
        foreach (GameObject piece in pieces)
        {
            if (outOfBounds.IsTouching(piece.GetComponentOfType<CompositeCollider2D>()))
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
}
