using System;
using Unisave.Facades;
using UnityEngine;


public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int score = 0 ;

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

    private void persist(){
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
    
    private void retrieve(){
        //get the score form the database
    }

    public int getScore(){
        return score;
    }
}
