using System;
using System.Collections.Generic;
using Unisave.Facades;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerData _playerData;
    Mission _currentMission;
    float _gameStartTime;
    
    void Awake()
    {
        _playerData = new PlayerData();
    }

    void OnEnable()
    {
        GameEvents.GameStarted += OnGameStarted;
        GameEvents.MissionStarted += OnMissionStarted;
        GameEvents.SequenceProcessed += OnSequenceProcessed;
        GameEvents.GameEnded += OnGameEnded;
    }

    void OnDisable()
    {
        GameEvents.GameStarted -= OnGameStarted;
        GameEvents.MissionStarted -= OnMissionStarted;
        GameEvents.SequenceProcessed -= OnSequenceProcessed;
        GameEvents.GameEnded -= OnGameEnded;
    }

    void OnGameStarted()
    {
        _gameStartTime = Time.realtimeSinceStartup;
    }

    void OnMissionStarted(Mission mission)
    {
        _currentMission = mission;
        _playerData.MissionChoices.Add(_currentMission.name, new List<string>());
    }

    void OnSequenceProcessed(string choiceName)
    {
        if (_currentMission == null)
            throw new Exception("Current mission is null");
        
        if (!_playerData.MissionChoices.ContainsKey(_currentMission.name))
            throw new Exception("Current mission has not been added.");
        
        _playerData.MissionChoices[_currentMission.name].Add(choiceName);
    }

    void OnGameEnded()
    {
        _playerData.ScoreTotal = FindObjectOfType<ScoreManager>().Score;
        
        _playerData.MinutesPlayed = Time.realtimeSinceStartup - _gameStartTime;
        
        var userId = PlayerPrefs.GetString("userEntityId");

        if (string.IsNullOrWhiteSpace(userId))
            throw new Exception("User has not been created.");
        
        OnFacet<GameDataFacet>.Call(nameof(GameDataFacet.Create), userId, _playerData.ScoreTotal, _playerData.MissionChoices, _playerData.MinutesPlayed).Done();
    }
}

public class PlayerData
{
    public int ScoreTotal { get; set; }
    public Dictionary<string, List<string>> MissionChoices { get; set; }
    public float MinutesPlayed { get; set; }
}