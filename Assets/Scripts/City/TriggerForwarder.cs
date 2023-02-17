using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class TriggerForwarder : MonoBehaviour
{
    [SerializeField]
    private IDBase[] _target;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!collision.gameObject.CompareTag("Player"))
            return;
        foreach (IDBase target in _target)
            target.OnTrigger();
    }
    private void OnValidate()
    {

        _target = new List<IDBase>(GetComponentsInParent<IDBase>()).ToArray();
        if (!TryGetComponent<Collider2D>(out Collider2D coll) || !coll.isTrigger)
            Debug.LogError("Collider missing or not trigger on object");
    }
}
