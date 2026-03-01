using TMPro;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Counter : MonoBehaviour
{
    
    private TextMeshProUGUI scoreText;

    public static int mergedItemsCounter = 0;
    private int scoreCounter = 0;
    private static int totalScore = 0;

    private void Awake()
    {
        scoreText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        scoreText.text = "0000";
        Merge.Merged += CountScore;
    }
    private void OnDestroy()
    {
        Merge.Merged -= CountScore;
    }
    private void CountScore(GameObject newItem)
    {
        mergedItemsCounter++;
        scoreCounter = 100;
        totalScore += scoreCounter;
        scoreText.text=totalScore.ToString().PadLeft(4,'0');
        Debug.Log($"Total SCORE: {totalScore}");
        Debug.Log($"Total merged veggies: {mergedItemsCounter}");
    }
}