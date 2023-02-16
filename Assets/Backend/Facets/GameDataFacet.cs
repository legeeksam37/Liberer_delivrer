using System.Collections.Generic;
using System.Linq;
using Unisave.Facades;
using Unisave.Facets;

public class GameDataFacet : Facet
{
    public void Create(string userEntityId, int scoreTotal, Dictionary<string, List<string>> missionChoices, float minutesPlayed)
    {
        var gameDataEntity = new GameDataEntity {
            userEntity = DB.Find<UserEntity>(userEntityId),
            finalScore = scoreTotal,
            minutesPlayed = minutesPlayed,
            missionResults = missionChoices.Select(kvp => new MissionResult {
                missionName = kvp.Key,
                choices = kvp.Value,
            }).ToList()
        };

        gameDataEntity.Save();
    }
}