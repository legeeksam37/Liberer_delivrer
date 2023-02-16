using Unisave.Facets;

public class GameDataFacet : Facet
{
    public void Create(string userEntityId, PlayerData playerData)
    {
        var gameDataEntity = new GameDataEntity();
        //TODO: gameDataEntity.Save();
    }
}