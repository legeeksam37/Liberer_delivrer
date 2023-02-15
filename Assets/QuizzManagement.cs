using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizzManagement : MonoBehaviour
{
    public bool trouve;
    public bool rate;

    public GameObject trouvePannel;
    public GameObject ratepannel;
    public GameObject basePannel;


    void Start()
    {
        trouve = false;
        rate = false;
    }

    public void NextButton()
    {
        if(rate == true)
        {
            Debug.Log("raté");
            basePannel.SetActive(false);
            ratepannel.SetActive(true);
        }
        else if(trouve == true)
        {
            Debug.Log("trouve");
            basePannel.SetActive(false);
            trouvePannel.SetActive(true);
        }
    }

    public void Boutun()
    {
        rate = true;
        trouve = false;
        Debug.Log("1");
    }

    public void Boutdeux()
    {
        rate = true;
        trouve = false;
        Debug.Log("2");
    }

    public void Bouttrois()
    {
        trouve = true;
        rate = false;
        Debug.Log("3");
    }
}
