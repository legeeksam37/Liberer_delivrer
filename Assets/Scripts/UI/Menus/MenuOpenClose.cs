using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuOpenClose : MonoBehaviour
{
    private Animator _animator;
    private Button _button;
    
    private void Start()
    {
        _animator = GetComponentInParent<Animator>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ToggleMenu);
    }

    void ToggleMenu()
    {
        _animator.SetBool("IsOpen", !_animator.GetBool("IsOpen"));
    }
    
}
