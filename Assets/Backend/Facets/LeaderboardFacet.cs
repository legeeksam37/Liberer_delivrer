using System.Linq;
using Unisave.Facades;
using Unisave.Facets;

public class LeaderboardFacet : Facet
{
    public void Add(int score)
    {
        var leaderboardEntry = DB.TakeAll<LeaderboardEntryEntity>()
            .Filter(le => le.score == score)
            .FirstOrCreate();

        leaderboardEntry.score = score;
        leaderboardEntry.count++;
        
        leaderboardEntry.Save();
    }
    
    public int GetPercentileRanking(int score)
    {
        var leaderboardEntryEntities = DB.TakeAll<LeaderboardEntryEntity>().Get();

        var totalScoresCount = leaderboardEntryEntities.Sum(le => le.count) - 1; // Don't count our own score

        if (totalScoresCount <= 0)
            return 0;
        
        var worstScoresCount = leaderboardEntryEntities.Where(le => le.score < score).Sum(le => le.count);
        
        return (int) ((1 - worstScoresCount / (float) totalScoresCount) * 100);
    }
}
