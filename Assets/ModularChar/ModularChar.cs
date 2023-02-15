using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class ModularChar : MonoBehaviour
{
    string commune;
    
  
    public int IndexHat = 0;
    public int IndexBody = 0;
    public int IndexLegs = 0;

    
    public TextMeshProUGUI InputCommune;
    public TextMeshProUGUI InputAge;

    //-------------Hat
    [Space(10)]
    public Button LeftArrowHat;
    public Button RightArrowhat;
    public SpriteRenderer Hat;
    //-------------Body
    [Space(10)]
    public Button LeftArrowBody;
    public Button RightArrowBody;
    public SpriteRenderer Body;
    //-------------Legs
    [Space(10)]
    public Button LeftArrowLegs;
    public Button RightArrowLegs;
    public SpriteRenderer Legs;

    //------------tab mat
    [Space(10)]
    public Material[] materials;
    int MaxMat;

    public GameObject Character;

    [SerializeField]
    GameObject warning;
    private void Start()
    {
        Hat.material = materials[IndexHat];
        Body.material = materials[IndexBody];
        Legs.material = materials[IndexLegs];
        MaxMat = materials.Length;
       

    }
    public void OnclickleftArrHat()
    {
        if (--IndexHat == -1) { IndexHat = 2; }
        Hat.material = materials[IndexHat];
      

    }
    public void OnclickRightArrHat()
    {
        if (++IndexHat == MaxMat) { IndexHat = 0; }
        Hat.material = materials[IndexHat];
        
    }
    public void OnclickleftArrBody()
    {
        if (--IndexBody == -1) { IndexBody = 2; }
        Body.material = materials[IndexBody];

        
    }
    public void OnclickRightArrBody()
    {
        if (++IndexBody == MaxMat) { IndexBody = 0; }
        Body.material = materials[IndexBody];
       
    }
    public void OnclickleftArrLegs()
    {
        if (--IndexLegs == -1) { IndexLegs = 2; }
        Legs.material = materials[IndexLegs];
        
    }
    public void OnclickRightArrLegs()
    {
        if (++IndexLegs == MaxMat) { IndexLegs = 0; }
        Legs.material = materials[IndexLegs];
        
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
