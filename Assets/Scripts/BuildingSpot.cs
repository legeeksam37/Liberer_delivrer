using System.Collections;
using System.Collections.Generic;
using ScenarioStructures;
using UnityEngine;

public class BuildingSpot : MonoBehaviour
{
    [SerializeField] private int SocialLimit = 0;
    [SerializeField] private GameObject UnderLimit;
    [SerializeField] private GameObject OverLimit;

    void Start()
    {
        CheckScore();
        ScoreManager.Singleton.ScoreUpdated += OnScoreUpdated;
    }

    void OnScoreUpdated(int newScore)
    {
        CheckScore();
    }

    void CheckScore()
    {
        if(ScoreManager.Singleton._scoreSoc<SocialLimit)
        {
            UnderLimit.SetActive(true);
            OverLimit.SetActive(false);
        }
        else
        {
            UnderLimit.SetActive(false);
            OverLimit.SetActive(true);
        }
    }
}
