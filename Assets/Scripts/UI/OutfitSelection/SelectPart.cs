using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPart : MonoBehaviour
{
    [SerializeField] private CustomizationElementType elementType;
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    
    private ModularChar modularChar;
    
    private int _currentItem = 1;

    private void Awake()
    {
        modularChar = GetComponentInParent<ModularChar>();
    }

    public void NextItem()
    {
        Debug.Log("Before     : " + _currentItem + "   " + sprites.Count);
        if(_currentItem + 1 > sprites.Count)
        {
            _currentItem = 1;
        }
        else
        {
            _currentItem++;
        }
        
        Debug.Log(_currentItem + "   " + sprites.Count);
        modularChar.ChangeItem(elementType, sprites[_currentItem - 1]);
    }

    public void PreviousItem()
    {
        Debug.Log("after     : " + _currentItem + "   " + sprites.Count);
        if(_currentItem - 1 < 1)
        {
            _currentItem = sprites.Count;
        }
        else
        {
            _currentItem--;
        }
        Debug.Log(_currentItem + "   " + sprites.Count);
        modularChar.ChangeItem(elementType, sprites[_currentItem - 1]);
        
    }
    
}
