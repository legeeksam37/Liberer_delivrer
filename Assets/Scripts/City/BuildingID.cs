using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingID : MonoBehaviour
{
    [SerializeField]
    private BuildingType _type;
    [SerializeField]
    private Transform _transformOverride;
    public Transform TransformOverride => _transformOverride==null ? transform : _transformOverride;

    public BuildingType Type { get => _type; }
}
