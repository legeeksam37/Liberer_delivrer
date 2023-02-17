using System.Collections;
using ScenarioStructures;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] birds;
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
        GameEvents.ScenarioEnded += RefreshSpawnRate;
        RefreshSpawnRate();
    }

    // Update is called once per frame
    void Update()
    {
        currDelay -= Time.deltaTime;
        if (currDelay <= 0)
        {
            SpawnBird();
            currDelay += spawnDelay;
        }
    }

    public void SpawnBird()
    {
        float xrand = Random.Range(min.x, max.x);
        float yrand = Random.Range(min.y, max.y);
        GameObject b = Instantiate(birds[Random.Range(0, birds.Length)]); 
        b.transform.position = new Vector3(xrand, yrand, 0);
    }

    public void RefreshSpawnRate()
    {
        int score = ScoreManager.Singleton._scoreEnv;
        if (score <= 0)
            spawnDelay = float.MaxValue;
        else
            spawnDelay = 10 / (float)score;
        currDelay = spawnDelay;
    }
    public void RefreshSpawnRate((string message, Result result) tuple)
    {
        RefreshSpawnRate();
    }
}
