using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyNPC : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
    }
}
