using DG.Tweening.Plugins.Options;
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
    [SerializeField, Range(0, 5)] private int _currentMissionIndex;
    private IDisplay _display;
    public Mission Mission => _missions[_currentMissionIndex];

    public int CurrentMissionIndex
    {
        set
        {
            _currentMissionIndex = value;
            NewMission();
        }
    }
    private void HandleEventRaised(int index)
    {
        Debug.Log("We don't operate any checks on the type on type of enum reicved, we consider we always get the correct one and process");
        if (Mission.ProcessSequenceAbsolute(index))
            UpdateDisplayByCurrentState();
        else
        {
            var final = Mission.Current.choice as FinalNode;
            _display.Collapse();
            GameEvents.ScenarioEnded?.Invoke((final.Message, final.Result));
        }
    }

    private void UpdateDisplayByCurrentState()
    {
        var choice = Mission.Current.choice as Choice;
        switch (choice.Type)
        {
            case Choicetypes.OnlineOrLive: _display.OnlineOrLive(Mission.GetOptions<OnlineOrLive>()); break;
            case Choicetypes.TravelMethod: _display.Travel(Mission.GetOptions<TravelMethod>()); break;
            case Choicetypes.WithdrawalType: _display.WithDrawal(Mission.GetOptions<WithdrawalType>()); break;
            case Choicetypes.DelayType: _display.Delay(Mission.GetOptions<DelayType>()); break;
        }
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
    }

    private void Update()
    {
        if (false && Input.GetMouseButtonDown(0))
        {
            Mission.ProcessSequenceRandom();
        }
    }
}
