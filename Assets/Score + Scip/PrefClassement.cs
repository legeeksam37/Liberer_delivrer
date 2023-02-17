using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrefClassement : MonoBehaviour
{
    public TextMeshProUGUI placement;
    public TextMeshProUGUI name;
    public TextMeshProUGUI score;
    public TextMeshProUGUI pourcentage;

    public GameObject gold;
    public GameObject iron;
    public GameObject bronze;
    
    public void SetAllStat(string place, string names, string scoring)
    {
        placement.text = place;
        name.text = names;
        score.text = "score : " + scoring;

        ScoreManager.GetInstance().GetPercentileRanking(int.Parse(scoring), percentile =>
        {
            pourcentage.enabled = true;
            pourcentage.text = $"{percentile}%";
        });
    }
}
