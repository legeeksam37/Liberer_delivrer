using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehaviour : MonoBehaviour
{
    [SerializeField] float speed = 1.5f;
    private Vector3 scaleChange, positionChange;
    // Start is called before the first frame update
    void Start()
    {
        float rand = Random.Range(2,6);
        scaleChange = new Vector3(-0.1f, -0.1f, -0.1f)* rand;
        positionChange = new Vector3(0.0f, -0.05f, 0.0f)* rand;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(speed*Time.deltaTime, 0, 0));
        transform.localScale += scaleChange * Time.deltaTime;
        transform.position += positionChange * Time.deltaTime;
        if (transform.localScale.y < 0.3f || transform.localScale.y > 0.78f)
        {
            scaleChange = -scaleChange;
            positionChange = -positionChange;
        }
    }
}
