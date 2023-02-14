using System;
using UnityEngine;

public class Smartphone : MonoBehaviour
{
    [SerializeField] RectTransform _rectTransform;

    [SerializeField] GameObject _orderSelection;
    [SerializeField] GameObject _withdrawalSelection;
    [SerializeField] GameObject _delayTypeSelection;
    [SerializeField] GameObject _travelMethodSelection;
    private GameObject _currentPanel;

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
    public void Delay() => ChangePanel(_delayTypeSelection);
    public void Travel() => ChangePanel(_travelMethodSelection);
    public void Order() => ChangePanel(_withdrawalSelection);
    #region ButtonCallbacks
    public void SetWithdrawalType(int withdrawalType)
    {
        Delay();
        GameEvents.WithdrawalTypeSelected?.Invoke((WithdrawalType)withdrawalType);
    }

    public void SetDelayType(int delayType)
    {
        Travel();
        GameEvents.DelayTypeSelected?.Invoke((DelayType)delayType);
    }
    public void SetTravelMethod(int travelMethod)
    {
        GameEvents.TravelMethodSelected?.Invoke((TravelMethod)travelMethod);
        CutsceneManager.Instance.Play(CutsceneType.Delivery);

        Collapse();
    }
    #endregion

    private void ChangePanel(GameObject newPanel)
    {
        _currentPanel.SetActive(false);
        _currentPanel = newPanel;
        _currentPanel.SetActive(true);
    }
}