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
        OnFacet<ScoreFacet>
            .Call<ScoreEntity>(
                nameof(ScoreFacet.PostScore),
                score
            )
            .Done();
    }

    void GetPercentileRanking(Action<int> onCompleted)
    {
        OnFacet<ScoreFacet>
            .Call<int>(
                nameof(ScoreFacet.GetPercentileRanking),
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