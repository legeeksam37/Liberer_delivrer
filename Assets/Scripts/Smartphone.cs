using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Smartphone : MonoBehaviour, IDisplay
{
    [SerializeField] private CutsceneManager _cutscene;
    
    [SerializeField] JoystickControls joystick;

    [SerializeField] RectTransform _rectTransform;

    [SerializeField] GameObject _orderSelection;
    [SerializeField] GameObject _withdrawalSelection;
    [SerializeField] GameObject _delayTypeSelection;
    [SerializeField] GameObject _travelMethodSelection;

    [SerializeField] GameObject _logo1;
    Image _imageLogo1;
    [SerializeField] GameObject _logo2;
    Image _imageLogo2;
    [SerializeField] GameObject _logo3;
    Image _imageLogo3;
    [SerializeField] GameObject _logo4;
    Image _imageLogo4;
    
    private IDisplay _display;
    
    TMP_Text currentText;
    private GameObject _currentPanel;
    private bool _isExpanded = true;

    private MissionManager _missionManager;
    
    private void Awake()
    {
        GameEvents.MissionStarted += (m) => ChangeIcon(m.Logo);
        _imageLogo1 = _logo1.GetComponent<Image>();
        _imageLogo2 = _logo2.GetComponent<Image>();
        _imageLogo3 = _logo3.GetComponent<Image>();
        _imageLogo4 = _logo4.GetComponent<Image>();
    }

    private void ChangeIcon(Sprite logo)
    {
        _imageLogo1.sprite = logo;
        _imageLogo2.sprite = logo;
        _imageLogo3.sprite = logo;
        _imageLogo4.sprite = logo;
    }

    private void OnEnable()
    {
        _missionManager = MissionManager.GetInstance();
        SceneManager.sceneLoaded += (scene, mode) => StartPhone(scene);
    }

    private void OnDestroy()
    {
        _missionManager.OnMissionsStarted -= Expand;
    }

    private void StartPhone(Scene scene)
    {
        if (scene.name != "Game") return;
        SetupPhoneDisplay();
        Expand();
    }
    
    public void Expand()
    {
        _rectTransform.anchoredPosition = new Vector3(280f, 250f);
        joystick.enabled = false;
        _isExpanded = true;
    }

    public void Collapse()
    {
        _rectTransform.anchoredPosition = new Vector3(280f, -350f);
        joystick.enabled = true;
        _isExpanded = false;
    }
    public void OnlineOrLive(RecursiveEnabledChoice currentStep, List<OnlineOrLive> options = null) => ChangePanel(_orderSelection,options?.Select(e=>(int)e).ToHashSet(),currentStep);
    public void Delay(RecursiveEnabledChoice currentStep, List<DelayType> options = null) => ChangePanel(_delayTypeSelection, options?.Select(e => (int)e).ToHashSet(),currentStep);
    public void Travel(RecursiveEnabledChoice currentStep, List<TravelMethod> options = null) => ChangePanel(_travelMethodSelection, options?.Select(e => (int)e).ToHashSet(),currentStep);
    public void WithDrawal(RecursiveEnabledChoice currentStep, List<WithdrawalType> options = null) => ChangePanel(_withdrawalSelection, options?.Select(e => (int)e).ToHashSet(),currentStep);
    #region ButtonCallbacks
    public void SetWithdrawalType(int withdrawalType)
    {
        //Delay();
        GameEvents.WithdrawalTypeSelected?.Invoke((WithdrawalType)withdrawalType);
    }

    public void SetDelayType(int delayType)
    {
        //Travel();
        GameEvents.DelayTypeSelected?.Invoke((DelayType)delayType);
    }
    public void SetTravelMethod(int travelMethod)
    {
        GameEvents.TravelMethodSelected?.Invoke((TravelMethod)travelMethod);
        CutsceneManager.Instance.Play(CutsceneType.Delivery);
    }
    public void SetOnlineOLive(int onlineOrLive)
    {
        GameEvents.OnlineOrLiveSelected?.Invoke((OnlineOrLive)onlineOrLive);
    }
    #endregion
    private void HandleBuildingReached(BuildingID obj)
    {
        if (_missionManager.mission.TargetedBuilding == obj.Type)
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
        Debug.Log(_display);
        GameEvents.WithdrawalTypeSelected += (e) => HandleEventRaised((int)e);
        GameEvents.DelayTypeSelected += (e) => HandleEventRaised((int)e);
        GameEvents.TravelMethodSelected += (e) => HandleEventRaised((int)e);
        GameEvents.OnlineOrLiveSelected += HandleOnlineOrLive;
        GameEvents.ScenarioEnded += (sr) => Debug.Log("Colee says : " + sr.message + " with result : " + sr.result);
        GameEvents.BuildingReached += HandleBuildingReached;
        GameEvents.TravelReached += HandleTravelReached;
        UpdateDisplayByCurrentState();
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
    private void UpdateDisplayByCurrentState()
    {
        var choice = _missionManager.mission.Current.choice as Choice;
        switch (choice.Type)
        {
            case Choicetypes.OnlineOrLive: _display.OnlineOrLive(_missionManager.mission.Current, _missionManager.mission.GetOptions<OnlineOrLive>()); break;
            case Choicetypes.TravelMethod: _display.Travel(_missionManager.mission.Current, _missionManager.mission.GetOptions<TravelMethod>()); break;
            case Choicetypes.WithdrawalType: _display.WithDrawal(_missionManager.mission.Current, _missionManager.mission.GetOptions<WithdrawalType>()); break;
            case Choicetypes.DelayType: _display.Delay(_missionManager.mission.Current, _missionManager.mission.GetOptions<DelayType>()); break;
        }
    }

    
    private void HandleEventRaised(int index)
    {
        Debug.Log("We don't operate any checks on the type on type of enum reicved, we consider we always get the correct one and process");
        if (_missionManager.mission.ProcessSequenceAbsolute(index))
            UpdateDisplayByCurrentState();
        else
        {
            var final = _missionManager.mission.Current.choice as FinalNode;
            if (final != null)
            {
                _display.Collapse();
                GameEvents.ScenarioEnded?.Invoke((final.Message, final.Result));
            }
            else
                Debug.Log("Error happend in scenario processing, check upper messages");
        }
    }
    private void ChangePanel(GameObject newPanel, HashSet<int> options, RecursiveEnabledChoice currentStep)
    {
        if (!_isExpanded)
            return;
        _currentPanel?.SetActive(false);
        _currentPanel = newPanel;
        TMP_Text[] allText = newPanel.GetComponentsInChildren<TMP_Text>();
        allText[allText.Length - 1 ].text = currentStep.message;
        _currentPanel.SetActive(true);
        //Debug.Log(" Infos : " + currentStep.message);
        //We only display options that we want
        Transform parent = _currentPanel.transform.GetChild(0);
        for (int i = 0; i < parent.childCount; i++)
            //If options are null we consider we wan all options
            parent.GetChild(i).gameObject.SetActive(options==null || options.Contains(i));     
    }
  
}