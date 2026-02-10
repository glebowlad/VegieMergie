using TMPro;
using System;
using UnityEngine;
using UnityEditorInternal;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditorInternal.Profiling.Memory.Experimental;

public class Counter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    public static int mergedItemsCounter = 0;
    private int scoreCounter = 0;
    private static int totalScore = 0;

    private void Awake()
    {
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
        scoreCounter = (int) Math.Pow(2, Array.IndexOf(InternalEditorUtility.tags, newItem.tag)-8);
        totalScore += scoreCounter;
        scoreText.text=totalScore.ToString();
        Debug.Log($"Total SCORE: {totalScore}");
        Debug.Log($"Total merged veggies: {mergedItemsCounter}");
    }
}