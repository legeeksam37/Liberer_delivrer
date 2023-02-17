using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPrefabSpawn : MonoBehaviour
{
    public GameObject playerPrefabSkin;

    private void OnEnable()
    {
        MissionManager.GetInstance().OnMissionsStarted += SpawnPrefab;
        SpawnPrefab();
    }

    private void OnDestroy()
    {
        MissionManager.GetInstance().OnMissionsStarted -= SpawnPrefab;
    }

    void SpawnPrefab()
    {
        Instantiate(playerPrefabSkin, transform);
    }
    
}
