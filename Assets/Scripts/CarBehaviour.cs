using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviour : MonoBehaviour
{
    [SerializeField]
    private Vector2 dir = Vector2.zero;
    private float speed = 1f;
    static float TurnDelay = 0.5f;
    [SerializeField]
    private GameObject[] carFacing = new GameObject[4];
    private int currentCarFace = 0;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var Facing in carFacing)
            Facing.SetActive(false);
        ChangeDir(dir);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir*speed*Time.deltaTime);
    }

    public void ChangeDir(Vector2 newDir)
    {
        dir = newDir;
        carFacing[currentCarFace].SetActive(false);
        if (dir.x * dir.x > dir.y * dir.y)
            if (dir.x > 0)
                currentCarFace = 1;
            else
                currentCarFace = 3;
        else
            if (dir.y > 0)
                currentCarFace = 0;
            else
                currentCarFace = 2;
        carFacing[currentCarFace].SetActive(true);
    }

    public void TurnRight()
    {
        ChangeDir(new Vector2(dir.y,-dir.x));
    }
    public void DelayTurnLeft()
    {
        Invoke("TurnLeft", TurnDelay/speed);
    }

    public void TurnLeft()
    {
        ChangeDir(new Vector2(-dir.y, dir.x));
    }
}
