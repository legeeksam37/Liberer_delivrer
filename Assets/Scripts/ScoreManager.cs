using System;
using Unisave.Facades;
using UnityEngine;


public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int score;

    void Start()
    {
        retrieve();
    }

    private int increase(){
        return score++;
    }

    private int decrease(){
        return score--;
    }

    [ContextMenu("Persist")]
    void Persist(){
        OnFacet<LeaderboardFacet>
            .Call(
                nameof(LeaderboardFacet.Add),
                score
            )
            .Done();
    }

    void GetPercentileRanking(Action<int> onCompleted)
    {
        OnFacet<LeaderboardFacet>
            .Call<int>(
                nameof(LeaderboardFacet.GetPercentileRanking),
                score
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
        return score;
    }
}
