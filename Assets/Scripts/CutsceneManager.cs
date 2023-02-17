using System;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] Cutscene _bikeCutscene;
    [SerializeField] Cutscene _busCutscene;
    [SerializeField] Cutscene _shopCutscene;
    [SerializeField] Cutscene _walkCutscene;
    [SerializeField] Cutscene _carCutscene;
    [SerializeField] Cutscene _deliveryCutscene;
    
    public static CutsceneManager Instance { get; set; }
    
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        GameEvents.TravelReached += (t)=>TravelCutscene(t.Type);
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void TravelCutscene(TravelMethod type)
    {
        switch (type)
        {
            case TravelMethod.None:
                break;
            case TravelMethod.Bus: 
                Play(CutsceneType.Bus);
                break;
            case TravelMethod.Car:
                Play(CutsceneType.Car);
                break;
            case TravelMethod.Bike:
                Play(CutsceneType.Bike);
                break;
            case TravelMethod.Walk:
                break;
            default:
                break;
        }
    }

    public void Play(CutsceneType cutsceneType)
    {
        Debug.Log("Play cutscene " + cutsceneType);
        switch (cutsceneType)
        {
            case CutsceneType.Walk:
                StartCoroutine(_walkCutscene.Play());
                break;
            case CutsceneType.Bus:
                StartCoroutine(_busCutscene.Play());
                break;
            case CutsceneType.Car:
                StartCoroutine(_carCutscene.Play());
                break;
            case CutsceneType.Bike:
                StartCoroutine(_bikeCutscene.Play());
                break;
            case CutsceneType.Shop:
                StartCoroutine(_shopCutscene.Play());
                break;
            case CutsceneType.Delivery:
                StartCoroutine(_deliveryCutscene.Play());
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(cutsceneType), cutsceneType, null);
        }
    }
}