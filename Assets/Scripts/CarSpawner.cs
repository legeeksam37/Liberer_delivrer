using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] toSpawn;
    private Transform[] spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint= new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            spawnPoint[i] = transform.GetChild(i);
        Invoke("SpawnCar",0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCar()
    {
        int rand = Random.Range(0, spawnPoint.Length);
        GameObject g = Instantiate(toSpawn[Random.Range(0, toSpawn.Length)], spawnPoint[rand]);
        CarBehaviour c = g.GetComponent<CarBehaviour>();
        c.ChangeDir(spawnPoint[rand].GetComponent<CarSpawnPoint>().dir);

        Invoke("SpawnCar",1);
    }
}
