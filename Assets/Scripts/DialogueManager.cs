using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DialogueManager : MonoBehaviour
{

    public TextMeshProUGUI textComponent;
    public string[] lines;
    private float textSpeed = 0.2f;
    private int index;
    
        // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        startDialogue();

    }

    // Update is called once per frame
    void Update()
    {
        if(textComponent.text == lines[index]){
            NextLine();
        }else 
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
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
        if(index < lines.Length - 2){
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
    }
}
