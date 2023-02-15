using System;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField] float _initialValue;
    
    public float Timer { get; private set; }
    public bool IsActive { get; private set; }
    
    public Action TimerStarted { get; set; }
    public Action TimerOver { get; set; }

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

    [ContextMenu("Begin")]
    public void Begin()
    {
        Timer = _initialValue;
        IsActive = true;
        
        TimerStarted?.Invoke();
    }

    public void Pause()
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
