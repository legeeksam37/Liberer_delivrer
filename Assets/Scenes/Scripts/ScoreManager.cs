using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager
{
    [SerializeField]
    private int score = 0 ;


    JSONSaver js = new JSONSaver();

    // public ScoreManager(int score){
    //     this.score = score;
    // }


    // Start is called before the first frame update
    void Start()
    {
        retrieve();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int increase(){
        return score++;
    }

    private int decrease(){
        return score--;
    }

    private void persist(){
        //send the score to the datebase

    }
    
    private void retrieve(){
        //get the score form the database
    }

    public int getScore(){
        return score;
    }

    
}
