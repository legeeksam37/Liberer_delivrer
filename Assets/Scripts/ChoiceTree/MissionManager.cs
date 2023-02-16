using DG.Tweening.Plugins.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
/// <summary>
/// Monobehaviour having references to all the mission, especially current one that you can get and cal AdvanceMission
/// </summary>
public class MissionManager : MonoBehaviour
{
    [SerializeField] private Mission[] _missions;
    [SerializeField, Range(0, 7)] private int _currentMissionIndex;
    [SerializeField] private CutsceneManager _cutscene;
    private IDisplay _display;
    public Mission Mission => _missions[_currentMissionIndex];
    private void OnValidate()
    {
        if(_cutscene==null)
            _cutscene = FindObjectOfType<CutsceneManager>();
        if (_cutscene == null)
            Debug.Log("Didn't find a CutsceneManager in scene, add one");
    }
    public int CurrentMissionIndex
    {
        set
        {
            _currentMissionIndex = value;
            NewMission();
        }
        get { return _currentMissionIndex; }
    }
    private void HandleEventRaised(int index)
    {
        Debug.Log("We don't operate any checks on the type on type of enum reicved, we consider we always get the correct one and process");
        if (Mission.ProcessSequenceAbsolute(index))
            UpdateDisplayByCurrentState();
        else
        {
            var final = Mission.Current.choice as FinalNode;
            if (final != null)
            {
                _display.Collapse();
                GameEvents.ScenarioEnded?.Invoke((final.Message, final.Result));
            }
            else
                Debug.Log("Error happend in scenario processing, check upper messages");
        }
    }

    private void UpdateDisplayByCurrentState()
    {
        var choice = Mission.Current.choice as Choice;
        switch (choice.Type)
        {
            case Choicetypes.OnlineOrLive: _display.OnlineOrLive(Mission.Current, Mission.GetOptions<OnlineOrLive>()); break;
            case Choicetypes.TravelMethod: _display.Travel(Mission.Current, Mission.GetOptions<TravelMethod>()); break;
            case Choicetypes.WithdrawalType: _display.WithDrawal(Mission.Current, Mission.GetOptions<WithdrawalType>()); break;
            case Choicetypes.DelayType: _display.Delay(Mission.Current, Mission.GetOptions<DelayType>()); break;
        }
    }
    private void HandleBuildingReached(BuildingID obj)
    {
        if (Mission.TargetedBuilding == obj.Type)
        {
            HandleEventRaised((int)TravelMethod.Walk);
            _cutscene.TravelCutscene(TravelMethod.Walk);
        }
    }
    private void HandleTravelReached(TravelID obj)
    {
        HandleEventRaised((int)obj.Type);
    }
    private void Awake()
    {
        //Find an IDisplay implementaiton in scene and use it
        FindObjectsOfType<MonoBehaviour>().FirstOrDefault(go => go.TryGetComponent<IDisplay>(out _display));
        GameEvents.WithdrawalTypeSelected += (e) => HandleEventRaised((int)e);
        GameEvents.DelayTypeSelected += (e) => HandleEventRaised((int)e);
        GameEvents.TravelMethodSelected += (e) => HandleEventRaised((int)e);
        GameEvents.OnlineOrLiveSelected += (e) => HandleEventRaised((int)e);
        GameEvents.ScenarioEnded += (sr) => Debug.Log("Colee says : " + sr.message + " with result : " + sr.result);
        GameEvents.BuildingReached += HandleBuildingReached;
        GameEvents.TravelReached += HandleTravelReached;
        _display.Expand();
    }


    private void Start()
    {
        NewMission();
    }

    private void NewMission()
    {
        Mission.Init();
        UpdateDisplayByCurrentState();
        GameEvents.MissionStarted.Invoke(Mission);
    }

    private void Update()
    {
       /* if (false && Input.GetMouseButtonDown(0))
        {
            Mission.ProcessSequenceRandom();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            CurrentMissionIndex++;
        }*/
    }
}
