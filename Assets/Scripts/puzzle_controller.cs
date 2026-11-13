using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class puzzle_controller : MonoBehaviour
{
    private CompositeCollider2D outOfBounds;
    private List<GameObject> pieces = new List<GameObject>();
    private List<Transform>[] extraGoals = new List<Transform>[2];
    private bool currentlyCompleted;
    // public bool currentlyOptCompleted;
    private int minCount;
    private int maxCount;
    public BoxCollider2D entrance;
    private Collider2D[] hit;
    public movement player;
    public camera_follow camera;
    private Transform cameraTarget;
    public float cameraSize;
    public LayerMask piecesLayer;
    public bool completed;
    public Tilemap tiles;
    public Vector2Int clearLocationStart;
    public Vector2Int clearLocationEnd;
    // Start is called before the first frame update
    void Start()
    {
        //find all the objects
        outOfBounds = transform.gameObject.GetComponent<CompositeCollider2D>();
        foreach (Transform child in transform.Find("Pieces"))
        {
            pieces.Add(child.gameObject);
        }
        extraGoals[0] = new List<Transform>();
        extraGoals[1] = new List<Transform>();
        // extraGoals[2] = new List<Transform>();
        // extraGoals[3] = new List<Transform>();
        foreach (Transform child in transform.Find("ExtraGoals").Find("High"))
        {
            extraGoals[0].Add(child);
        }
        foreach (Transform child in transform.Find("ExtraGoals").Find("Low"))
        {
            extraGoals[1].Add(child);
        }
        // foreach (Transform child in transform.Find("ExtraGoals").Find("OptHigh"))
        // {
        //     extraGoals[2].Add(child);
        // }
        // foreach (Transform child in transform.Find("ExtraGoals").Find("OptLow"))
        // {
        //     extraGoals[3].Add(child);
        // }

        entrance = transform.Find("Entrance").GetComponent<BoxCollider2D>();
        cameraTarget = transform.Find("Camera Target");

        if (clearLocationStart.x > clearLocationEnd.x)
        {
            (clearLocationStart.x, clearLocationEnd.x) = (clearLocationEnd.x, clearLocationStart.x);
        }
        if (clearLocationStart.y > clearLocationEnd.y)
        {
            (clearLocationStart.y, clearLocationEnd.y) = (clearLocationEnd.y, clearLocationStart.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // hit = new Collider2D[0];
        // Physics2D.OverlapCollider(entrance, new ContactFilter2D().NoFilter(), hit);
        // foreach (Collider2D collider in hit)
        // {
        //     if (collider.GameObject().CompareTag("Player"))
        //     {
        //         enterPuzzle(collider.GameObject());
        //     }
        // }
        checkCompletion();
        resolveCompletion();
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player") && !completed)
        {
            enterPuzzle();
        }
    }

    void enterPuzzle()
    {
        player.active = false;
        camera.follow = cameraTarget;
        camera.targetSize = cameraSize;
    }

    void checkCompletion()
    {
        currentlyCompleted = true;
        // currentlyOptCompleted = true;
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
        if (minCount > 2 && minCount < int.MaxValue)
        {
            currentlyCompleted = false;
        }
        // (minCount, maxCount) = scanGoals(extraGoals[2]);
        // if (minCount < 6)
        // {
        //     currentlyOptCompleted = false;
        // }
        // (minCount, maxCount) = scanGoals(extraGoals[3]);
        // if (minCount > 2)
        // {
        //     currentlyOptCompleted = false;
        // }
    }
    void resolveCompletion()
    {
        if (currentlyCompleted && !completed)
        {
            completed = true;
            exitPuzzle();
            clearCells();
        }
    }
    void exitPuzzle()
    {
        player.active = true;
        camera.follow = player.gameObject.transform;
        camera.targetSize = 5;
    }
    void clearCells()
    {
        for (int x = clearLocationStart.x; x < clearLocationEnd.x; x++)
        {
            for (int y = clearLocationStart.y; y < clearLocationEnd.y; y++)
            {
                tiles.SetTile(new Vector3Int(x, y, 0), null);
            }
        }
    }
    (int, int) scanGoals(List<Transform> positions)
    {
        // Scan the goals and return the minimum and maximum counts of nearby game objects on the boxes layer
        int minCount = int.MaxValue;
        int maxCount = int.MinValue;
        foreach (Transform pos in positions)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(pos.position, new Vector2(1, 1), 0f, piecesLayer);
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
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 1f);
        Gizmos.DrawCube(
            (Vector2)(clearLocationStart+clearLocationEnd)/2,
            new Vector3(
                Mathf.Abs(clearLocationEnd.x-clearLocationStart.x),
                Mathf.Abs(clearLocationEnd.y-clearLocationStart.y),
                1
            )
        );
    }
}
