using System.Linq;
using Unisave.Facades;
using Unisave.Facets;

public class GameDataFacet : Facet
{
    public void Create(string userEntityId, PlayerData playerData)
    {
        var gameDataEntity = new GameDataEntity {
            userEntity = DB.Find<UserEntity>(userEntityId),
            finalScore = playerData.ScoreTotal,
            minutesPlayed = playerData.MinutesPlayed,
            missionResults = playerData.MissionChoices.Select(kvp => new MissionResult {
                missionName = kvp.Key,
                choices = kvp.Value,
            }).ToList()
        };

        gameDataEntity.Save();
    }
}