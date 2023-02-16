using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/MarketPlaceItemObject", order = 1)]
public class MarketplaceItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public float itemPrice;
    public Sprite itemSprite;
}
