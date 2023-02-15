using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WheelManager : MonoBehaviour
{
    public GameObject Wheel;
    public int WheelSpeed = 30;

    Text TextButton;
    public Button ButtonStart;
    // Start is called before the first frame update
    void Start()
    {
        TextButton = ButtonStart.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickStart()
    {
       
    }
    
    IEnumerator RotateWheel()
    {
        transform.Rotate(0, 0, WheelSpeed * Time.deltaTime);
        yield return null;
    }
}
