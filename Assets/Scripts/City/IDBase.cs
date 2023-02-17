using System;
using UnityEngine;

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
    public abstract void OnTrigger();
}