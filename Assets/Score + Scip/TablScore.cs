using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TablScore : MonoBehaviour
{
    public List<int> scoreList = new List<int>();
    public GameObject prefName;

    public List<string> nameList = new List<string>();

    void Start()
    {
        SetTablScore(28, "macchado");
    }

    public void SetTablScore(int playerScoring, string playerNaming)
    {
        scoreList.Add(playerScoring);

        scoreList.Sort();
        int x = 5;

        for (int i = 1; i < scoreList.Count + 1; i++)
        {
            GameObject gm = Instantiate(prefName, this.transform);

            if (scoreList[x] == playerScoring)// 250 111 71
            {
                gm.GetComponent<PrefClassement>().SetAllStat("" + i, playerNaming, "" + scoreList[x]);
                gm.GetComponent<PrefClassement>().placement.color = new Color(0f, 0f , 0f);
                gm.GetComponent<PrefClassement>().name.color = new Color(0f, 0f , 0f);
                gm.GetComponent<PrefClassement>().score.color = new Color(0f, 0f , 0f);
            }
            else
            {
                gm.GetComponent<PrefClassement>().SetAllStat("" + i, nameList[x], "" + scoreList[x]);
            }

            if (i == 1)
            {
                gm.GetComponent<PrefClassement>().gold.SetActive(true);
            }

            if (i == 2)
            {
                gm.GetComponent<PrefClassement>().iron.SetActive(true);
            }

            if (i == 3)
            {
                gm.GetComponent<PrefClassement>().bronze.SetActive(true);
            }



            x--;
        }
    }

}   
