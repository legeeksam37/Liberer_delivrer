using System.Collections;
using System.Collections.Generic;
using ScenarioStructures;
using UnityEngine;

public class PollutionSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] clouds;
    [SerializeField] private float spawnDelay = 1f;
    private Vector2 min;
    private Vector2 max;
    float currDelay = 0;
    // Start is called before the first frame update
    void Start()
    {
        Collider2D c = GetComponent<Collider2D>();
        min = c.bounds.min;
        max = c.bounds.max; 
        RefreshSpawnRate();
        ScoreManager.Singleton.ScoreUpdated += OnScoreUpdated;
    }

    // Update is called once per frame
    void Update()
    {
        currDelay -= Time.deltaTime;
        if (currDelay <= 0)
        {
            SpawnPollution();
            currDelay += spawnDelay;
        }
    }

    public void SpawnPollution()
    {
        float xrand = Random.Range(min.x, max.x);
        float yrand = Random.Range(min.y, max.y);
        GameObject b = Instantiate(clouds[Random.Range(0, clouds.Length)]);
        b.transform.position = new Vector3(xrand, yrand, 0);
    }

    public void RefreshSpawnRate()
    {
        int score = ScoreManager.Singleton._scoreEnv;
        if (score >= 0)
            spawnDelay = float.MaxValue;
        else
            spawnDelay = 10 / (float)-score;
        currDelay = spawnDelay;
    }

    void OnScoreUpdated(int newScore)
    {
        RefreshSpawnRate();
    }
}
