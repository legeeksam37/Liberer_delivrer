using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Level : MonoBehaviour
{
    [SerializeField]
    private List<MissionItem> missions = new List<MissionItem>();

    public bool showMissionsOnAwake = false;

    private Button _levelButton;
    
    private void Awake()
    {
        if(showMissionsOnAwake) SetActiveLevel();
        _levelButton = GetComponent<Button>();
        _levelButton.onClick.AddListener(SetActiveLevel);
    }

    private void Update()
    {
        if (LevelSelector.CurrentLevelSelected != this) return;
        _levelButton.Select();
    }

    public void SetActiveLevel()
    {
        if (LevelSelector.CurrentLevelSelected != null) 
            LevelSelector.CurrentLevelSelected.RemoveMissionsFromView();
        
        LevelSelector.CurrentLevelSelected = this;
        ShowMissionsInView();
    }

    private void ShowMissionsInView()
    {
        foreach (var missionItem in missions)
        {
                missionItem.gameObject.SetActive(true);
        }
    }

    private void RemoveMissionsFromView()
    {
        foreach (var missionItem in missions)
        {
            missionItem.gameObject.SetActive(false);
        }
    }
    
}
