using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONSaver : MonoBehaviour
{
    // private ScoreManager sm;
    private string path ="";
    private string finalPath = "";
    string savePath = "";
    string json = "";
    private int current_score;

    void Start()
    {
        // getPlayerScore();
        setPath();
        saveData();
        loadData();
        Debug.Log("start");
    }

    void Update()
    {

    }
    

    /*     public void getPlayerScore(){
        sm = new ScoreManager(120);
    } */

    // public int getPlayerScore(){
    //     return sm.getScore();
    // }

    public void setPath(){
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "Score.json";
        finalPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Score.json";
    }

    public void saveData(ScoreManager sm){
        savePath = path;
        string json = JsonUtility.ToJson(sm);
        using StreamWriter writer = new StreamWriter(finalPath);
        writer.Write(json);
    }

    public int loadData(){
        using StreamReader reader = new StreamReader(finalPath);
        json = reader.ReadToEnd();
        ScoreManager s = JsonUtility.FromJson<ScoreManager>(json);
        current_score = s.getScore();
       return current_score;
    }

}
