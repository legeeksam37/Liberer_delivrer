using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizzManager : MonoBehaviour
{
    public Quizz[] arrayquizz;
    public int quizzIndex;
    public int selectedSolution = 3;

    public Button a, b, c;

    public Color on, off;

    public TMP_Text question;

    public TMP_Text reponseUn;
    public TMP_Text reponseDeux;
    public TMP_Text reponseTrois;

    public TMP_Text reponseUnSolution;
    public TMP_Text reponseDeuxSolution;
    public TMP_Text reponseTroisSolution;

    public TMP_Text solution;

    public int resultat;

    public GameObject panelQuestion, panelSolution;



    public void Next()
    {
        if(selectedSolution >= 0 && selectedSolution <= 2)
        {
            panelQuestion.SetActive(false);
            panelSolution.SetActive(true);

            if (selectedSolution == resultat)
            {
                solution.text = arrayquizz[quizzIndex].solutionWinner;
            }
            else
            {
                solution.text = arrayquizz[quizzIndex].solutionLooser;
            }

            if(resultat == 0)
            {
                reponseUnSolution.color = Color.green;
                reponseDeuxSolution.color = Color.red;
                reponseTroisSolution.color = Color.red;
                reponseUnSolution.transform.parent.GetComponent<Outline>().effectColor = Color.green;
                reponseDeuxSolution.transform.parent.GetComponent<Outline>().effectColor = Color.red;
                reponseTroisSolution.transform.parent.GetComponent<Outline>().effectColor = Color.red;
            }
            else if (resultat == 1)
            {
                reponseUnSolution.color = Color.red;
                reponseDeuxSolution.color = Color.green;
                reponseTroisSolution.color = Color.red;
                reponseUnSolution.transform.parent.GetComponent<Outline>().effectColor = Color.red;
                reponseDeuxSolution.transform.parent.GetComponent<Outline>().effectColor = Color.green;
                reponseTroisSolution.transform.parent.GetComponent<Outline>().effectColor = Color.red;
            }
            else if (resultat == 2)
            {
                reponseUnSolution.color = Color.red;
                reponseDeuxSolution.color = Color.red;
                reponseTroisSolution.color = Color.green;
                reponseUnSolution.transform.parent.GetComponent<Outline>().effectColor = Color.red;
                reponseDeuxSolution.transform.parent.GetComponent<Outline>().effectColor = Color.red;
                reponseTroisSolution.transform.parent.GetComponent<Outline>().effectColor = Color.green;
            }
        }
    }

    public void SelectOption(int index)
    {
        switch (index)
        {
            case 0:
                a.interactable = false;
                b.interactable = true;
                c.interactable = true;
                a.gameObject.GetComponent<Image>().color = off;
                b.gameObject.GetComponent<Image>().color = on;
                c.gameObject.GetComponent<Image>().color = on;
                selectedSolution = index;
                break;
            case 1:
                a.interactable = true;
                b.interactable = false;
                c.interactable = true;
                a.gameObject.GetComponent<Image>().color = on;
                b.gameObject.GetComponent<Image>().color = off;
                c.gameObject.GetComponent<Image>().color = on;
                selectedSolution = index;
                break;
            case 2:
                a.interactable = true;
                b.interactable = true;
                c.interactable = false;
                a.gameObject.GetComponent<Image>().color = on;
                b.gameObject.GetComponent<Image>().color = on;
                c.gameObject.GetComponent<Image>().color = off;
                selectedSolution = index;
                break;
            default:
                break;
        }
    }

    public void Quit()
    {
        panelSolution.SetActive(false);
    }

    public void OpenQuizz(int index)
    {
        quizzIndex = index;
        question.text = arrayquizz[quizzIndex].question;
        reponseUn.text = arrayquizz[quizzIndex].reponseUn;
        reponseDeux.text = arrayquizz[quizzIndex].reponseDeux;
        reponseTrois.text = arrayquizz[quizzIndex].reponseTrois;
        reponseUnSolution.text = arrayquizz[quizzIndex].reponseUn;
        reponseDeuxSolution.text = arrayquizz[quizzIndex].reponseDeux;
        reponseTroisSolution.text = arrayquizz[quizzIndex].reponseTrois;
        resultat = arrayquizz[quizzIndex].resultat;
    }

    void OnEnable()
    {
        OpenQuizz(0);
    }
}
