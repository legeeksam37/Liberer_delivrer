using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_ColeeAnim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetTrigger("Text_PopUp");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
