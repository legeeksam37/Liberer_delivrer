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
        //Debug.Log(_markerPrefab);
        //_markerPrefab.Custom(Quest.secondaryColor, .5f * Vector3.one);
        GameEvents.MissionStarted += (m) => _markerPrefab.callQuest(this);
    }
    public void OnMissionStart()
    {
        _markerPrefab = Instantiate(_markerPrefab, transform);
        _markerPrefab.Custom(Quest.secondaryColor, .33f * Vector3.one);
        _markerPrefab.callQuest(this);
    }
    public void ChangeState(bool state)
    {
        _markerPrefab.gameObject.SetActive(state);
        GetComponentInChildren<Collider2D>().enabled = state;
    }
    public override void OnTrigger()
    {
        GameEvents.TravelReached.Invoke(this);
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        _markerPrefab = AssetDatabase.LoadAssetAtPath<Quest>("Assets/Prefabs/Quest.prefab");
#endif
    }

}
