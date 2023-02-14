using UnityEngine;

public class Transport : MonoBehaviour
{
    [SerializeField] TravelMethod _travelMethod;
    [SerializeField] CutsceneType _cutsceneType;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Player _))
        {
            GameEvents.TravelMethodSelected?.Invoke(_travelMethod);
            CutsceneManager.Instance.Play(_cutsceneType);
        }
    }
}