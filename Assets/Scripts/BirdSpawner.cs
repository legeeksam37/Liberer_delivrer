using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] birds;
    private Vector2 min;
    private Vector2 max;
    private float spawnDelay = 1f;
    float currDelay = 0;
    // Start is called before the first frame update
    void Start()
    {
        Collider2D c = GetComponent<Collider2D>();
        min = c.bounds.min;
        max = c.bounds.max;
        SpawnBird();
    }

    // Update is called once per frame
    void Update()
    {
        if (currDelay <= 0)
        {
            SpawnBird();
            currDelay += spawnDelay;
        }
        else
            currDelay -= Time.deltaTime;
    }

    public void SpawnBird()
    {
        float xrand = Random.Range(min.x, max.x);
        float yrand = Random.Range(min.y, max.y);
        GameObject b = Instantiate(birds[Random.Range(0, birds.Length)]); 
        b.transform.position = new Vector3(xrand, yrand, 0);
    }
}
