using System.Linq;
using Unisave.Facades;
using Unisave.Facets;

public class ScoreFacet : Facet
{
    public void Persist(int score)
    {
        var scoreEntity = new ScoreEntity {
            score = score
        };
        scoreEntity.Save();
    }
    
    public int GetPercentileRanking(int score)
    {
        var scores = DB.TakeAll<ScoreEntity>().Get();

        var totalScoresCount = scores.Count - 1; // Don't count our own score

        if (totalScoresCount <= 0)
            return 0;
        
        var worstScoresCount = scores.Count(s => s.score < score);
        
        return (int) ((1 - worstScoresCount / (float) totalScoresCount) * 100);
    }
}