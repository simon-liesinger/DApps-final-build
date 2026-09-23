using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piece_builder : MonoBehaviour
{
    public enum PieceType {T, L, R, O, I, S, Z};
    public PieceType variant;
    public Transform[] parts = new Transform[5];
    public Vector2Int pentominoPosition;
    public bool pentomino;
    void OnValidate()
    {
        parts[4].gameObject.SetActive(pentomino);
        parts[4].localPosition = (Vector2)pentominoPosition;
        switch (variant)
        {
            case PieceType.T:
                parts[1].localPosition = new Vector3(-1,  0, 0);
                parts[2].localPosition = new Vector3( 1,  0, 0);
                parts[3].localPosition = new Vector3( 0, -1, 0);
                break;
            case PieceType.L:
                parts[1].localPosition = new Vector3( 0,  1, 0);
                parts[2].localPosition = new Vector3( 0, -1, 0);
                parts[3].localPosition = new Vector3( 1, -1, 0);
                break;
            case PieceType.R:
                parts[1].localPosition = new Vector3( 0,  1, 0);
                parts[2].localPosition = new Vector3( 0, -1, 0);
                parts[3].localPosition = new Vector3(-1, -1, 0);
                break;
            case PieceType.O:
                parts[1].localPosition = new Vector3( 1,  0, 0);
                parts[2].localPosition = new Vector3( 0,  1, 0);
                parts[3].localPosition = new Vector3( 1,  1, 0);
                break;
            case PieceType.I:
                parts[1].localPosition = new Vector3( 0,  1, 0);
                parts[2].localPosition = new Vector3( 0, -1, 0);
                parts[3].localPosition = new Vector3( 0,  2, 0);
                break;
            case PieceType.S:
                parts[1].localPosition = new Vector3(-1,  0, 0);
                parts[2].localPosition = new Vector3( 0,  1, 0);
                parts[3].localPosition = new Vector3( 1,  1, 0);
                break;
            case PieceType.Z:
                parts[1].localPosition = new Vector3( 1,  0, 0);
                parts[2].localPosition = new Vector3( 0,  1, 0);
                parts[3].localPosition = new Vector3(-1,  1, 0);
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
