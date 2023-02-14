using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
/// <summary>
/// Monobehaviour having references to all the mission, especially current one that you can get and cal AdvanceMission
/// </summary>
public class MissionManager : MonoBehaviour
{
    [SerializeField] private Mission[] _missions;
    [SerializeField, Range(0, 5)] private int _currentMissionIndex;
    public Mission Mission => _missions[_currentMissionIndex];

    public int CurrentMissionIndex
    {
        set
        {
            _currentMissionIndex = value;
            Mission.Init();
        }
    }

    [SerializeField]
    private VisualNovelManager _visualNovelManager;
    private void Start()
    {
        Mission.Init();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Mission.ProcessSequenceRandom();
            Debug.Log("Mission : " + Mission.CurrentName);
        }
    }
}
