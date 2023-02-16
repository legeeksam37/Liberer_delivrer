using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class IDBase<T> : IDBase where T : Enum
{

    [SerializeField]
    protected T _type;

    public T Type { get => _type; }

}
public abstract class IDBase : MonoBehaviour
{
    [SerializeField]
    private Transform _transformOverride;
    public Transform TransformOverride => _transformOverride == null ? transform : _transformOverride;
    public abstract void OnTriggerEnter2D(Collider2D collision);
}