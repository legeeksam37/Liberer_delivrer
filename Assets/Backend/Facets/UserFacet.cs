using Unisave.Facets;

public class UserFacet : Facet
{
    public void Persist(int age, string city)
    {
        var userEntity = new UserEntity {
            age = age,
            city = city
        };
        userEntity.Save();
    }
}