using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MissionItem : MonoBehaviour
{
    public bool isLocked = false;
    
    [SerializeField] private Image lockImage;
    
    private Button _missionsButton;
    private void Awake()
    {
        _missionsButton = GetComponent<Button>();
        ToggleLockMission();
    }

    void ToggleLockMission()
    {
        isLocked = !isLocked;
        if (lockImage == null)
        {
            Debug.LogWarning("No lock image set.");
            return;
        }
        
        lockImage.gameObject.SetActive(isLocked);
        _missionsButton.enabled = !isLocked;
    }
    
}
