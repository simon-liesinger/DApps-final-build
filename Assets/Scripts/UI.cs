using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public movement playerStats;
    public Text hitCounter;
    public Text enemiesDestroyedCounter;
    public Text timePassed;
    public Canvas canvas;
    public float timer = 0f;
    // Start is called before the first frame update
    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<movement>();

        
    }

    // Update is called once per frame
    private void Update()
    {
        hitCounter.text = $"Hits taken: {playerStats.timesHit}";
        enemiesDestroyedCounter.text = $"Enemies destroyed: {playerStats.enemiesSlain}";
        timer += 1f * Time.deltaTime;
        timePassed.text = $" {(Mathf.Floor(timer*10))/10}";
        
        
    }

}
