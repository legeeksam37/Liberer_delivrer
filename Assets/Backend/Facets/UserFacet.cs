using System;
using Unisave.Facades;
using Unisave.Facets;

public class UserFacet : Facet
{
    public string Create(int age, string city)
    {
        var userEntity = new UserEntity {
            age = age,
            city = city,
            connectionCount = 1,
        };

        userEntity.Save();

        return userEntity.EntityId;
    }

    public UserEntity Get(string entityId)
    {
        var userEntity = DB.Find<UserEntity>(entityId);

        if (userEntity is null)
            throw new Exception("User not found.");

        return userEntity;
    }

    public void Update(UserEntity userEntity)
    {
        userEntity.Save();
    }
}