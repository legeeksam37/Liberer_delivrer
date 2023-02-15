using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingID : MonoBehaviour
{
    [SerializeField]
    private BuildingType _type;

    public BuildingType Type { get => _type; }
}
