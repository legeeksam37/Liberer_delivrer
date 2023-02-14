using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizzInteraction : MonoBehaviour
{

    bool isFalse;
    bool isRight;
    public GameObject WinPannel;
    public GameObject LoosePannel;
    public GameObject BasePannel;


    void Update()
    {
        if (isFalse==true)
        {
            BasePannel.SetActive(false);
            WinPannel.SetActive(true);
        }
    }
}
