using ScenarioStructures;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.Build.Content;
#endif
using UnityEngine;

public class ColeeManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Animator>().SetTrigger("AnimStart");
    }

    private void Awake()
    {
        GameEvents.ScenarioEnded += colee;
    }

    public void colee((string message,Result) t)
    {
         GetComponent<Animator>().SetTrigger("AnimStart");
    }
}
