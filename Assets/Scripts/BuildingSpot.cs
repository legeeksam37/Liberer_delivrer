using System.Collections;
using System.Collections.Generic;
using ScenarioStructures;
using UnityEngine;

public class BuildingSpot : MonoBehaviour
{
    [SerializeField] private int SocialLimit = 0;
    [SerializeField] private GameObject UnderLimit;
    [SerializeField] private GameObject OverLimit;
    // Start is called before the first frame update
    void Start()
    {
        CheckScore();
        GameEvents.ScenarioEnded += CheckScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CheckScore((string message, Result result) tuple)
    {
        CheckScore();
    }

    public void CheckScore()
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
