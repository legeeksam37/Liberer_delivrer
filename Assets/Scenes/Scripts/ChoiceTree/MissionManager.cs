using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Monobehaviour having references to all the mission, especially current one that you can get and cal AdvanceMission
/// </summary>
public class MissionManager : MonoBehaviour
{
    [SerializeField] private Mission[] _missions;
    [SerializeField, Range(0, 5)] private int _currentMissionIndex;
    public Mission _currentMission => _missions[_currentMissionIndex];
}
