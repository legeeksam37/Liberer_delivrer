using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using ScenarioStructures;
using TMPro;
using System.Linq;

public class DialogueManager : MonoBehaviour
{

    public TextMeshProUGUI textComponent;
    public string[] lines;
    private float textSpeed = 0.1f;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        //startDialogue();

    }

    private void Awake()
    {
        GameEvents.ScenarioEnded += dialogue;
        gameObject.SetActive(false);
    }

    public void dialogue((string message, Result) tuple)
    {
        var result = tuple.Item2;
        List<string> list = tuple.message.Split('\n').ToList();
        list.Add("Voici " + result.ScoreEnvironmental + " colee environnementaux, et " + result.ScoreSocial + " colee sociaux");
        lines = list.ToArray();
        textComponent.text = string.Empty;
        startDialogue();
    }


    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }*/
    }

    void startDialogue()
    {
        gameObject.SetActive(true);
        textComponent.text = string.Empty;
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        for (int index = 0; index < lines.Length; index++)
        {
            foreach (char c in lines[index].ToCharArray())
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
            textComponent.text += '\n';
            yield return new WaitForSeconds(.5f);
        }
    }


    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
