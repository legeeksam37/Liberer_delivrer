using ScenarioStructures;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class Test_ColeeAnim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetTrigger("AnimStart");
    }

    private void Awake()
    {
        GameEvents.ScenarioEnded += colee;
    }

    public void colee((string message,Result) t)
    {
         GetComponent<Animator>().SetTrigger("AnimStart");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
