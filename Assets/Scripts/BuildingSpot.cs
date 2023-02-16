using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpot : MonoBehaviour
{
    [SerializeField] private int SocialLimit = 0;
    [SerializeField] private GameObject UnderLimit;
    [SerializeField] private GameObject OverLimit;
    // Start is called before the first frame update
    void Start()
    {
        CheckScore();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckScore()
    {
        //if(GM.SocialScore < SocialLimit)
        {
            UnderLimit.SetActive(true);
            OverLimit.SetActive(false);
        }
       // else
        {
            UnderLimit.SetActive(false);
            OverLimit.SetActive(true);
        }
    }
}
