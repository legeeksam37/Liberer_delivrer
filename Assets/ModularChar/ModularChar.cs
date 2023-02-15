using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ModularChar : MonoBehaviour
{
    string Commune;
    int Age;
  
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
        
        JoystickControls JC = Character.AddComponent<JoystickControls>() as JoystickControls;
        PlayerManager.Instance.Player = this.Character;
        //SceneManager.MoveGameObjectToScene(Character, SceneManager.GetSceneByBuildIndex(1));
        SceneManager.LoadScene(1);
    }
}
