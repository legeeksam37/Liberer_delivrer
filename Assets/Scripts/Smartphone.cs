using System;
using UnityEngine;

public class Smartphone : MonoBehaviour
{
    [SerializeField] RectTransform _rectTransform;
    
    [SerializeField] GameObject _orderSelection;
    [SerializeField] GameObject _withdrawalSelection;
    [SerializeField] GameObject _delayTypeSelection;
    [SerializeField] GameObject _travelMethodSelection;

    void Start()
    {
        Expand();
    }

    public void Expand()
    {
        _rectTransform.anchoredPosition = new Vector3(280f, -90f);
    }
    
    public void Collapse()
    {
        _rectTransform.anchoredPosition = new Vector3(280f, -350f);
    }
    
    public void Order()
    {
        _orderSelection.SetActive(false);
        _withdrawalSelection.SetActive(true);
    }

    public void SetWithdrawalType(int withdrawalType)
    {
        _withdrawalSelection.SetActive(false);
        _delayTypeSelection.SetActive(true);
     
        GameEvents.WithdrawalTypeSelected?.Invoke((WithdrawalType) withdrawalType);
    }
    
    public void SetDelayType(int delayType)
    {
        _delayTypeSelection.SetActive(false);
        _travelMethodSelection.SetActive(true);
     
        GameEvents.DelayTypeSelected?.Invoke((DelayType) delayType);
    }
    
    public void SetTravelMethod(int travelMethod)
    {
        _travelMethodSelection.SetActive(false);
        _orderSelection.SetActive(true);
        
        GameEvents.TravelMethodSelected?.Invoke((TravelMethod) travelMethod);
        CutsceneManager.Instance.Play(CutsceneType.Delivery);
        
        Collapse();
    }
}