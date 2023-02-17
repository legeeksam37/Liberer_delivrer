using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TravelID : IDBase<TravelMethod>
{
    [SerializeField]
    private Quest _markerPrefab;
    private void OnEnable()
    {
        _markerPrefab = Instantiate(_markerPrefab, transform);
        Debug.Log(_markerPrefab);
        //_markerPrefab.Custom(Quest.secondaryColor, .5f * Vector3.one);
        GameEvents.MissionStarted += (m) => _markerPrefab.callQuest(this);
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        GameEvents.TravelReached.Invoke(this);
    }
    private void OnValidate()
    {
        _markerPrefab = AssetDatabase.LoadAssetAtPath<Quest>("Assets/Prefabs/Quest.prefab");

    }
}
