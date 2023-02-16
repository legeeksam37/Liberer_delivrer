using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class ModularChar : MonoBehaviour
{
    string commune;
    
    public TextMeshProUGUI InputCommune;
    public TextMeshProUGUI InputAge;

    public GameObject character;
    
    [SerializeField] GameObject warning;
    [SerializeField] private Image imagePreview;
    [SerializeField] private Button validateButton;

    private void Awake()
    {
        validateButton.onClick.AddListener(Validate);
    }

    public void ChangeItem(GameObject playerSkin)
    {
        imagePreview.sprite = playerSkin.GetComponent<SpriteRenderer>().sprite;
        character = playerSkin;
    }

    public void Validate()
    {
        //warning.SetActive(false);
        
        commune = InputCommune.text.Substring(0,InputCommune.text.Length - 1);
        
        MissionManager.GetInstance().movableCharacter.GetComponentInChildren<CharacterPrefabSpawn>().playerPrefabSkin = character;
        
        //Switch Menu to missions
        CanvasManager.GetInstance().SwitchCanvas(CanvasType.Missions);
        
        if (InputAge.text.Length <= 2 || InputAge.text.Length > 3 ||commune == "commune")
        {
            //warning.SetActive(true);
        }
    }

}
