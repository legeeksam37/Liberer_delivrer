using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField]
    Quest quest;
    // Start is called before the first frame update
    void Start()
    {
        quest.callQuest(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
