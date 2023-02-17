using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectPart : MonoBehaviour
{
    [SerializeField] private List<GameObject> PlayerSkin = new List<GameObject>();
    
    private ModularChar modularChar;
    
    private int _currentItem = 1;


    private void Awake()
    {
        modularChar = GetComponentInParent<ModularChar>();
    }

    private void OnEnable()
    {
        modularChar.ChangeItem(PlayerSkin[0]);
    }

    public void NextItem()
    {
        Debug.Log("Before     : " + _currentItem + "   " + PlayerSkin.Count);
        if(_currentItem + 1 > PlayerSkin.Count)
        {
            _currentItem = 1;
        }
        else
        {
            _currentItem++;
        }
        
        Debug.Log(_currentItem + "   " + PlayerSkin.Count);
        modularChar.ChangeItem(PlayerSkin[_currentItem - 1]);
    }

    public void PreviousItem()
    {
        Debug.Log("after     : " + _currentItem + "   " + PlayerSkin.Count);
        if(_currentItem - 1 < 1)
        {
            _currentItem = PlayerSkin.Count;
        }
        else
        {
            _currentItem--;
        }
        Debug.Log(_currentItem + "   " + PlayerSkin.Count);
        modularChar.ChangeItem(PlayerSkin[_currentItem - 1]);
        
    }

}
