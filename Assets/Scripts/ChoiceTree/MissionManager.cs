using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
/// <summary>
/// Monobehaviour having references to all the mission, especially current one that you can get and cal AdvanceMission
/// </summary>
public class MissionManager : MonoBehaviour
{
    [SerializeField] private Mission[] _missions;
    [SerializeField, Range(0, 5)] private int _currentMissionIndex;
    [SerializeField] private Smartphone _display;
    public Mission Mission => _missions[_currentMissionIndex];

    public int CurrentMissionIndex
    {
        set
        {
            _currentMissionIndex = value;
            Mission.Init();
        }
    }
    private void HandleEventRaised(int index)
    {
        Debug.Log("We don't operate any checks on the type on type of enum reicved, we consider we always get the correct one and process");
        Mission.ProcessSequenceAbsolute(index);
        Mission.Current.choice._type=Choicetypes.None;
    }
    private void Awake()
    {
        GameEvents.WithdrawalTypeSelected += (e) => HandleEventRaised((int)e);
        GameEvents.DelayTypeSelected += (e) => HandleEventRaised((int)e);
        GameEvents.TravelMethodSelected += (e) => HandleEventRaised((int)e);
    }
    private void Start()
    {
        Mission.Init();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Mission.ProcessSequenceRandom();
            Debug.Log("Mission : " + Mission.CurrentName);
        }
    }
}
