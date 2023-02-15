using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WheelManager : MonoBehaviour
{
    public GameObject Wheel;
    int IndexButton = 0;
    float WheelSpeed = 300;

    public TextMeshProUGUI TextButton;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
        //StopCoroutine("RotateWheel");
        IndexButton = 0;
        }
    }
    
    IEnumerator RotateWheel()
    {
        while (true)
        {
        Wheel.transform.Rotate(0, 0, WheelSpeed * Time.deltaTime);
        }
        yield return null;

        //StartCoroutine("RotateWheel");
    }
    IEnumerator SlowWheel() 
    {
        while(WheelSpeed > 0.5f) 
        {
            WheelSpeed /= 2 * Time.deltaTime;
        }
        yield return null;
       
    }
}
