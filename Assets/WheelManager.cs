using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WheelManager : MonoBehaviour
{
    public GameObject Wheel;
   
    BoxCollider2D BC;

    int IndexButton = 0;
    [SerializeField] float WheelSpeed = 300;

    public TextMeshProUGUI TextButton;
    public TextMeshProUGUI Result;
    
  
    public void OnClickStart()
    {
        if(IndexButton == 0)
        {
        TextButton.SetText("Stop");
        StartCoroutine("RotateWheel");
        IndexButton = 1;
        }
        else
        {
        TextButton.SetText("Faire tourner !");
        StartCoroutine("SlowWheel");
        
        IndexButton = 0;
        }
    }
    
    IEnumerator RotateWheel()
    {
       
        Wheel.transform.Rotate(0, 0, -WheelSpeed*Time.deltaTime,Space.World);
        
        yield return null;

        StartCoroutine("RotateWheel");
    }
    IEnumerator SlowWheel() 
    {
        TextButton.SetText("Patientez");
        
        while (WheelSpeed > 0.5f) 
        {
            WheelSpeed -= 12.5f;
            yield return new WaitForSecondsRealtime(0.5f);
        }
        StopCoroutine("RotateWheel");
        end();
        TextButton.SetText("Faire tourner !");
        yield return null;
       
    }
   
    void end()
    {
        
        WheelSpeed = 300;
        float RotFinal = Wheel.transform.localRotation.eulerAngles.z;
        Debug.Log(RotFinal);

        if(RotFinal <= 45 || RotFinal >= 315)
        {
            Debug.Log("Grey");
            Result.SetText("Bravo tu attends\n patiemment que le livreur\n garé en double-file devant toi\n finisse sa livraison sans t’énerver\n contre lui. \r\nTu gagnes 2 colees");


        }
        else if(RotFinal <= 135 && RotFinal > 45)
        {
            Debug.Log("light blue");
            Result.SetText("Aïe…Tu t’énerves contre un livreur garé en double file pourtant tu es bien heureux de trouver tes produits favoris dans ton commerce. \r\nTu perds 2 colees");

        }

        else if (RotFinal <= 225 && RotFinal > 135)
        {
            Debug.Log("orange");
            Result.SetText("Bravo tu attends patiemment que le livreur garé en double-file devant toi finisse sa livraison sans t’énerver contre lui. \r\nTu gagnes 2 colees");

        }
        else 
        {
            Debug.Log("dark blue");
            Result.SetText("Aïe…Tu t’énerves contre un livreur garé en double file pourtant tu es bien heureux de trouver tes produits favoris dans ton commerce. \r\nTu perds 2 colees");


        }
        RotFinal = 0;

    }
}
