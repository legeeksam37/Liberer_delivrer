using UnityEngine;

public class Transport : MonoBehaviour
{
    [SerializeField] TravelMethod _travelMethod;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log($"Travel method selected : {_travelMethod}");
        
        if (col.TryGetComponent(out Player _))
            GameEvents.TravelMethodSelected?.Invoke(_travelMethod);
    }
}