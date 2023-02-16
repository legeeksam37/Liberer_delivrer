using Unisave.Facades;
using UnityEngine;

public class ConnectionCounter : MonoBehaviour
{
    void Awake()
    {
        var userEntityId = PlayerPrefs.GetString("userEntityId");

        if (string.IsNullOrWhiteSpace(userEntityId))
        {
            Destroy(gameObject);
            return;
        }
        
        OnFacet<UserFacet>.Call<UserEntity>(nameof(UserFacet.Get), userEntityId)
            .Then(userEntity =>
                {
                    userEntity.connectionCount++;
                    OnFacet<UserFacet>.Call(nameof(UserFacet.Update), userEntity).Done();
                }
            )
            .Done();
    }
}