using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizzInteraction : MonoBehaviour
{
    bool bouton1;
    bool bouton2;
    bool bouton3;

    bool skip;
    bool change;
    public GameObject WinPannel;
    public GameObject LoosePannel;
    public GameObject BasePannel;


    void Update()
    {
        if (bouton1 == true)
        {
            Debug.Log("bouton1");
            BasePannel.SetActive(false);
            LoosePannel.SetActive(true);
            change = true;

            if(change == true)
            {
            skip = false;
            change = false;
            bouton1 = false;
            }
        }




        if (bouton2 == true)
        {
            if(skip == true)
            {
                Debug.Log("bouton2");
                BasePannel.SetActive(false);
                LoosePannel.SetActive(true);
                change = true;
            }

            if(change == true)
            {
                skip = false;
                change = false;
                bouton2 = true;
            }
        }



        if (bouton3 == true)
        {
        if(skip == true)
        {
            Debug.Log("bouton3");
            BasePannel.SetActive(false);
            WinPannel.SetActive(true);
            change = true;
        }
        if(change == true)
        {
            skip = false;
            change = false;
            bouton3 = true;
        }

    }
}

    public void Skip()
    {
        //Debug.Log(skip);
        skip = true;
    }

    public void next1()
    {
        bouton1 = true;
    }

        
    public void next2()
    {
      bouton2 = true;
    }


    public void next3() // bonne rép
    {
      bouton3 = true;
    }
}
