using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Quizz", menuName = "Quizz")]
public class Quizz : ScriptableObject
{
    public string question;

    public string reponseUn;
    public string reponseDeux;
    public string reponseTrois;

    public string solutionWinner;
    public string solutionLooser;

    
    public int resultat;
}
