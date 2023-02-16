using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Linq;

public class ModularChar : MonoBehaviour
{
    string commune;
    
    public TextMeshProUGUI InputCommune;
    public TextMeshProUGUI InputAge;

    public GameObject Character;
    
    [SerializeField] GameObject warning;
    [SerializeField] private Image imagePreview;
    
    
    public void ChangeItem(GameObject playerSkin)
    {
        imagePreview.sprite = playerSkin.GetComponent<SpriteRenderer>().sprite;
        Character = playerSkin;
    }

    public void Validate()
    {
        warning.SetActive(false);
        
        commune = InputCommune.text.Substring(0,InputCommune.text.Length - 1);
        
        PlayerManager.Instance.Player = this.Character;
      

        if (InputAge.text.Length <= 2 || InputAge.text.Length > 3 ||commune == "commune")
        {
            warning.SetActive(true);
           

        }
        else
        {
            JoystickControls JC = Character.AddComponent<JoystickControls>() as JoystickControls;
            
           
            //SceneManager.LoadScene(1);
        }
    }

}
