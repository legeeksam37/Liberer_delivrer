using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Smartphone : MonoBehaviour, IDisplay
{
    [SerializeField] RectTransform _rectTransform;

    [SerializeField] GameObject _orderSelection;
    [SerializeField] GameObject _withdrawalSelection;
    [SerializeField] GameObject _delayTypeSelection;
    [SerializeField] GameObject _travelMethodSelection;
    private GameObject _currentPanel;
    private void Awake()
    {
        GameEvents.MissionStarted += (m) => ChangeIcon(m.Logo);
    }

    private void ChangeIcon(Sprite logo)
    {
        throw new NotImplementedException("Find/add serialized ref to corresponding image object and set correct sprite, will be call on every new Mission");
    }

    void Start()
    {
        //Expand();
    }

    public void Expand()
    {
        _rectTransform.anchoredPosition = new Vector3(280f, -90f);
    }

    public void Collapse()
    {
        _rectTransform.anchoredPosition = new Vector3(280f, -350f);
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

    private void ChangePanel(GameObject newPanel, HashSet<int> options,RecursiveEnabledChoice currentStep)
    {
        _currentPanel?.SetActive(false);
        _currentPanel = newPanel;
        _currentPanel.SetActive(true);
        //Debug.Log(" Infos : " + currentStep.message);
        //We only display options that we want
        Transform parent = _currentPanel.transform;
        for (int i = 0; i < parent.childCount; i++)
            //If options are null we consider we wan all options
            _currentPanel.transform.GetChild(i).gameObject.SetActive(options==null || options.Contains(i));     
    }


    
}