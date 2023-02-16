using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelID : IDBase<TravelMethod>
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        GameEvents.TravelReached.Invoke(this);
    }
}
