using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intersection : MonoBehaviour
{

    public enum PositiveResults { LocalShops, Biodiversity, ClearSky, Traffic, HappyPeoples };
    public enum Sens { Haut,Bas,Gauche,Droite,EnFace};   

    public Sens Entree;

    public Sens[] Sortie;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 arrivalDir = collision.GetContact(0).normal;
        switch (Entree)
        {
            case Sens.Haut:
                if (arrivalDir != Vector2.down) return;
                break; 
            case Sens.Bas:
                if (arrivalDir != Vector2.up) return;
                break; 
            case Sens.Gauche:
                if (arrivalDir != Vector2.right) return;
                break; 
            case Sens.Droite:
                if (arrivalDir != Vector2.left) return;
                break;
            default: return;
        }
        CarBehaviour car = collision.gameObject.GetComponent<CarBehaviour>();
        switch(Sortie[Random.Range(0,Sortie.Length)])
        {
            case Sens.Droite:
                car.TurnRight(); 
                break;
            case Sens.Gauche:
                car.DelayTurnLeft();
                break;
            default: return;
        }          
        
    }
}
