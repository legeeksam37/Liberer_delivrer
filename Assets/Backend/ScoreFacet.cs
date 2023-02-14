using System.Linq;
using Unisave.Facades;
using Unisave.Facets;

public class ScoreFacet : Facet
{
    public ScoreEntity PostScore(int score)
    {
        var scoreEntity = new ScoreEntity();
        scoreEntity.score = score;
        scoreEntity.Save();
        return scoreEntity;
    }
    
    public int GetPercentileRanking(int score)
    {
        var scores = DB.TakeAll<ScoreEntity>().Get();

        var totalScoreCount = scores.Count;

        if (totalScoreCount == 0)
            return 0;
        
        var worstScoreCount = scores.Count(s => s.score < score);
        return (int) (worstScoreCount / (float) totalScoreCount * 100);
    }
}
