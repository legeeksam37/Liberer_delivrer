using DG.Tweening.Plugins.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

/// <summary>
/// Monobehaviour having references to all the mission, especially current one that you can get and cal AdvanceMission
/// </summary>
public class MissionManager : Singleton<MissionManager>
{
    public Mission mission; 

    
    [SerializeField] private CutsceneManager _cutscene;
    public GameObject movableCharacter;

    public delegate void MissionStarted();

    public event MissionStarted OnMissionsStarted;

    private void Start()
    {
        SceneManager.sceneLoaded += (scene, mode) => StartMission(scene);
    }

    private void OnValidate()
    {
        if(_cutscene==null)
            _cutscene = FindObjectOfType<CutsceneManager>();
        if (_cutscene == null)
            Debug.Log("Didn't find a CutsceneManager in scene, add one");
    }
    



    public void NewMission()
    {
        ScenesManager.GetInstance().LoadScene(ScenesManager.Scene.Game);
        CanvasManager.GetInstance().SwitchCanvas(CanvasType.InGameMenu);
        mission.Init();
        OnMissionsStarted?.Invoke();
    }

    private void StartMission(Scene scene)
    {
        if(scene.name == "Game")
        {
            GameEvents.MissionStarted?.Invoke(mission);
        } 
    }
}
