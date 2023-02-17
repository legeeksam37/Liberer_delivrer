using System.Collections;
using System.Collections.Generic;
using ScenarioStructures;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private float spawnDelay = 1f;
    [SerializeField] private GameObject[] toSpawn;
    private Transform[] spawnPoint;
    float currDelay = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint= new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            spawnPoint[i] = transform.GetChild(i);
        RefreshSpawnRate();
        GameEvents.ScenarioEnded += RefreshSpawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        currDelay -= Time.deltaTime;
        if (currDelay <= 0)
        {
            SpawnCar();
            currDelay += spawnDelay;
        }
    }

    public void SpawnCar()
    {
        int rand = Random.Range(0, spawnPoint.Length);
        GameObject g = Instantiate(toSpawn[Random.Range(0, toSpawn.Length)], spawnPoint[rand]);
        CarBehaviour c = g.GetComponent<CarBehaviour>();
        c.ChangeDir(spawnPoint[rand].GetComponent<CarSpawnPoint>().dir);
    }

    public void RefreshSpawnRate()
    {
        int score = ScoreManager.GetInstance()._scoreEnv;
        if (score >= 10)
            spawnDelay = float.MaxValue;
        else
            spawnDelay = Mathf.Clamp(Mathf.Exp(-score/10) , 0.4f , 4);
        currDelay = spawnDelay;
    }


    public void RefreshSpawnRate((string message, Result result) tuple)
    {
        RefreshSpawnRate();
    }

}
