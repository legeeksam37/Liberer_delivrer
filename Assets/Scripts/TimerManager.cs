using System;
using ScenarioStructures;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField] float _initialValue;
    
    public float Timer { get; private set; }
    public bool IsActive { get; private set; }
    
    public Action TimerStarted { get; set; }
    public Action TimerOver { get; set; }

    void OnEnable()
    {
        GameEvents.MissionStarted += OnMissionStarted;
        GameEvents.ScenarioEnded += OnScenarioEnded;
    }

    void Update()
    {
        if (!IsActive) 
            return;
        
        Timer -= Time.deltaTime;

        if (Timer <= 0f)
        {
            IsActive = false;
            Timer = 0f;
            TimerOver?.Invoke();
        }
    }

    void OnDisable()
    {
        GameEvents.MissionStarted -= OnMissionStarted;
        GameEvents.ScenarioEnded -= OnScenarioEnded;
    }

    void OnMissionStarted(Mission mission)
    {
        Begin();
    }

    void OnScenarioEnded((string message, Result result) tuple)
    {
        Stop();
    }

    public void Begin()
    {
        Timer = _initialValue;
        IsActive = true;
        
        TimerStarted?.Invoke();
    }

    public void Stop()
    {
        IsActive = false;
    }
    
    public void Resume()
    {
        IsActive = true;
    }

    public void Reset()
    {
        Timer = _initialValue;
        IsActive = false;
    }
}