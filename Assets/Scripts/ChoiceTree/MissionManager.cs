using DG.Tweening.Plugins.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Monobehaviour having references to all the mission, especially current one that you can get and cal AdvanceMission
/// </summary>
public class MissionManager : Singleton<MissionManager>
{
    public Mission mission; 
    private IDisplay _display;
    
    [SerializeField] private CutsceneManager _cutscene;
    public GameObject movableCharacter;

    public delegate void MissionStarted();

    public event MissionStarted OnMissionsStarted;
    
    private void OnValidate()
    {
        if (_cutscene == null)
            _cutscene = FindObjectOfType<CutsceneManager>();
        if (_cutscene == null)
            Debug.Log("Didn't find a CutsceneManager in scene, add one");
    }
    
    private void HandleEventRaised(int index)
    {
        Debug.Log("We don't operate any checks on the type on type of enum reicved, we consider we always get the correct one and process");
        if (mission.ProcessSequenceAbsolute(index)){
            UpdateDisplayByCurrentState();
        }
        else
        {
            var final = mission.Current.choice as FinalNode;
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
        var choice = mission.Current.choice as Choice;
        switch (choice.Type)
        {
            case Choicetypes.OnlineOrLive: _display.OnlineOrLive(mission.Current, mission.GetOptions<OnlineOrLive>()); break;
            case Choicetypes.TravelMethod: _display.Travel(mission.Current, mission.GetOptions<TravelMethod>()); break;
            case Choicetypes.WithdrawalType: _display.WithDrawal(mission.Current, mission.GetOptions<WithdrawalType>()); break;
            case Choicetypes.DelayType: _display.Delay(mission.Current, mission.GetOptions<DelayType>()); break;
        }
    }
    private void HandleBuildingReached(BuildingID obj)
    {
        if (mission.TargetedBuilding == obj.Type)
        {
            HandleEventRaised((int)TravelMethod.Walk);
            _cutscene.TravelCutscene(TravelMethod.Walk);
        }
    }
    private void HandleTravelReached(TravelID obj)
    {
        HandleEventRaised((int)obj.Type);
    }
    void SetupPhoneDisplay()
    {
        //Find an IDisplay implementaiton in scene and use it
        FindObjectsOfType<MonoBehaviour>().FirstOrDefault(go => go.TryGetComponent<IDisplay>(out _display));
        GameEvents.WithdrawalTypeSelected += (e) => HandleEventRaised((int)e);
        GameEvents.DelayTypeSelected += (e) => HandleEventRaised((int)e);
        GameEvents.TravelMethodSelected += (e) => HandleEventRaised((int)e);
        GameEvents.OnlineOrLiveSelected += HandleOnlineOrLive;
        GameEvents.ScenarioEnded += (sr) => Debug.Log("Colee says : " + sr.message + " with result : " + sr.result);
        GameEvents.BuildingReached += HandleBuildingReached;
        GameEvents.TravelReached += HandleTravelReached;
        _display.Expand();
    }
    private void HandleOnlineOrLive(OnlineOrLive e)
    {
        switch (e)
        {
            case global::OnlineOrLive.Live:
                GameEvents.GameStarted?.Invoke();
                _display.Collapse();
                break;
            default:
                break;
        }
        HandleEventRaised((int)e);
    }


    

    
    
    public void NewMission()
    {
        mission.Init();
        ScenesManager.GetInstance().LoadScene(ScenesManager.Scene.Game);
        OnMissionsStarted?.Invoke();
        GameEvents.MissionStarted?.Invoke(mission);
        //UpdateDisplayByCurrentState();
    }

}
