using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public movement playerStats;
    public int hitCounter;
    public Text testText;
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<movement>();
        testText.text = "FEGfhfewhfohew";

        
    }

    // Update is called once per frame
    void Update()
    {
        hitCounter = playerStats.timesHit;
        testText.text = $"Hits taken: {hitCounter}";


    }
}
