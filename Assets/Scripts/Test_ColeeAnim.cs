using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_ColeeAnim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetTrigger("AnimStart");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
