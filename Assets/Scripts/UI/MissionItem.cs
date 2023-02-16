using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MissionItem : MonoBehaviour
{
    public bool isLocked = false;
    [SerializeField] private Image lockImage;
    
    private Button _missionsButton;

    private delegate void LockStateChanged();

    private event LockStateChanged onLockStateChanged;

    [SerializeField]
    private ScenesManager.Scene sceneToLoad = ScenesManager.Scene.Game;
    
    private void Awake()
    {
        _missionsButton = GetComponent<Button>();
        _missionsButton.onClick.AddListener(LoadSceneFromManager);
    }

    private void Start()
    {
        onLockStateChanged += SetLockImageVisibility;
        onLockStateChanged += SetButtonInteractability;
        ToggleLockMission();
    }

    private void OnDestroy()
    {
        onLockStateChanged -= SetLockImageVisibility;
        onLockStateChanged -= SetButtonInteractability;
    }

    private void Update()
    {
        SetLockImageVisibility();
        SetButtonInteractability();
    }

    private bool ToggleLockMission()
    {
        isLocked = !isLocked;
        if (lockImage == null)
        {
            Debug.LogWarning("No lock image set.");
            return false;
        }
        onLockStateChanged?.Invoke();
        return isLocked;
    }

    private void SetLockImageVisibility()
    {
        lockImage.gameObject.SetActive(isLocked);
    }

    private void SetButtonInteractability()
    {
        _missionsButton.enabled = !isLocked;
    }

    private void LoadSceneFromManager()
    {
        ScenesManager.GetInstance().LoadScene(sceneToLoad);
    }
    
}
