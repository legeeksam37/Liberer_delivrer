using System;
using ScenarioStructures;
using Unisave.Facades;
using UnityEngine;


public class ScoreManager : MonoBehaviour
{
    public int _scoreEnv;
    public int _scoreSoc;

    public static ScoreManager Singleton;
    public int Score => _scoreEnv + _scoreSoc;
    
    public Action<int> ScoreUpdated { get; set; }
    
    void Start()
    {
        if (!Singleton)
            Singleton = this;
        else
            Destroy(this);
        DontDestroyOnLoad(this);
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

        ScoreUpdated?.Invoke(Score);
    }

    [ContextMenu("Persist")]
    void Persist(){
        OnFacet<LeaderboardFacet>
            .Call(
                nameof(LeaderboardFacet.Add),
                Score
            )
            .Done();
    }

    public void GetPercentileRanking(int score, Action<int> onCompleted)
    {
        OnFacet<LeaderboardFacet>
            .Call<int>(
                nameof(LeaderboardFacet.GetPercentileRanking),
                score
            )
            .Then(onCompleted)
            .Done();
    }
    
    public void GetPercentileRanking(Action<int> onCompleted)
    {
        OnFacet<LeaderboardFacet>
            .Call<int>(
                nameof(LeaderboardFacet.GetPercentileRanking),
                Score
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
