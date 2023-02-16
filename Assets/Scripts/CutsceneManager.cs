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

        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void Play(CutsceneType cutsceneType)
    {
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