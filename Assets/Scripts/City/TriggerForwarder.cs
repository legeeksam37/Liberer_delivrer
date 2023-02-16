using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class TriggerForwarder : MonoBehaviour
{
    [SerializeField]
    private BuildingID _target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _target.OnTriggerEnter2D(collision);
    }
    private void OnValidate()
    {
        if (_target == null)
            _target = GetComponentInParent<BuildingID>();
        if (!TryGetComponent<Collider2D>(out Collider2D coll) || !coll.isTrigger)
            Debug.LogError("Collider missing or not trigger on object");
    }
}
