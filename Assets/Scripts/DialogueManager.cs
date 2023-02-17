using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using ScenarioStructures;
using TMPro;


public class DialogueManager : MonoBehaviour
{

    public TextMeshProUGUI textComponent;
    public string[] lines;
    private float textSpeed = 0.1f;
    private int index;
    
        // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        startDialogue();

    }

    private void Awake()
    {
        GameEvents.ScenarioEnded += dialogue;
    }

    public void dialogue((string message, Result) t)
    {
        textComponent.text = string.Empty;
        startDialogue();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(textComponent.text == lines[index]){
                NextLine();
            }else 
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }

    }

    void startDialogue(){
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine(){
        foreach(char c in lines[index].ToCharArray()){
            textComponent.text +=c;
            yield return new WaitForSeconds(textSpeed);
        }
    }


    void NextLine(){
        if(index < lines.Length - 1){
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
