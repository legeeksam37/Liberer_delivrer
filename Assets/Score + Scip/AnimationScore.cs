using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationScore : MonoBehaviour
{
    [SerializeField]
    private int scorePoints;
    public GameObject pivot;

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    //rotation   -100 = z168   0 = z78  100 = -13
    public void Update() 
    {
        pivot.transform.eulerAngles = new Vector3(0, 0, map(Mathf.Clamp(scorePoints,-100,100), -100, 100, 168, -13));
    }

   

}
