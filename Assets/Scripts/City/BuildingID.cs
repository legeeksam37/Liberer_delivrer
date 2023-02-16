using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class BuildingID : IDBase<BuildingType>
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Collidd by ; " + collision.gameObject.name);
        GameEvents.BuildingReached?.Invoke(this);
    }
}
