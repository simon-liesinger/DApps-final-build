using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class piece_builder : MonoBehaviour
{
    public enum PieceType {T, L, R, O, I, S, Z};
    public PieceType variant;
    public List<Transform> parts = new List<Transform>{};
    public int extraPieces = 0;
    public List<Vector2Int> extraPositions = new List<Vector2Int>{};
    public GameObject piecePrefab;
    void OnValidate()
    {
        if (extraPieces < 0)
        {
            extraPieces = 0; // can't have less than 0
        }
        if (extraPieces+4 > parts.Count)
        {
            while (extraPieces+4 > parts.Count)
            {
                parts.Add(Instantiate(piecePrefab, transform).transform); // make some new ones to fill the count
                extraPositions.Add(new Vector2Int(0,0));
            }
        } else if (extraPieces+4 < parts.Count)
        {
            while (extraPieces+4 < parts.Count)
            {
                DestroyImmediate(parts[parts.Count-1].gameObject);
                parts.RemoveAt(parts.Count-1);
                extraPositions.RemoveAt(extraPositions.Count-1);
            }
        }
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
        for (int i = 0; i < extraPieces; i++)
        {
            parts[i+4].localPosition = (Vector2)extraPositions[i];
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
