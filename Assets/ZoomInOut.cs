using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class ZoomInOut : MonoBehaviour
{
    private Button _button;
    private bool _isZoomed = true;
    [SerializeField]private CinemachineVirtualCamera vcam1;
    [SerializeField]private CinemachineVirtualCamera vcam2;
    [SerializeField] private Animator _animator;
    
    
    
    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ToggleZoom);
    }

    void ToggleZoom()
    {
        Debug.Log(_isZoomed);
        _isZoomed = !_isZoomed;
        _animator.Play(_isZoomed ? "ZoomIn" : "ZoomOut");
        vcam1.Priority = (_isZoomed ? 1 : 0);
        vcam2.Priority = (_isZoomed ? 0 : 1);
    }
    
    
}
