using System;
using ScenarioStructures;
using Unisave.Facades;
using UnityEngine;


public class ScoreManager : MonoBehaviour
{
    int _scoreEnv;
    int _scoreSoc;

    public int Score => _scoreEnv + _scoreSoc;
    
    void Start()
    {
        retrieve();
    }

    void OnEnable()
    {
        GameEvents.ScenarioEnded += OnScenarioEnded;
    }

    void OnDisable()
    {
        GameEvents.ScenarioEnded -= OnScenarioEnded;
    }

    void OnScenarioEnded((string message, Result result) tuple)
    {
        _scoreEnv += tuple.result.ScoreEnvironmental;
        _scoreSoc += tuple.result.ScoreSocial;
    }

    [ContextMenu("Persist")]
    void Persist(){
        OnFacet<LeaderboardFacet>
            .Call(
                nameof(LeaderboardFacet.Add),
                _scoreSoc
            )
            .Done();
    }

    void GetPercentileRanking(Action<int> onCompleted)
    {
        OnFacet<LeaderboardFacet>
            .Call<int>(
                nameof(LeaderboardFacet.GetPercentileRanking),
                _scoreSoc
            )
            .Then(onCompleted)
            .Done();
    }

#if UNITY_EDITOR
    [ContextMenu("GetPercentileRanking")]
    void Debug_GetPercentileRanking()
    {
        GetPercentileRanking(percentile => Debug.Log($"Your score is in the top {percentile}% !"));
    }
#endif
    
    private void retrieve(){
        //get the score form the database
    }

    public int getScore(){
        return _scoreSoc;
    }
}
