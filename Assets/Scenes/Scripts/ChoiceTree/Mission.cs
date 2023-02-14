using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

[Serializable]
[CreateAssetMenu(fileName = "New mission", menuName = "Scenarisation/Mission")]
public class Mission : ScriptableObject
{
    [SerializeField]
    private Choice _rootChoice;
    [SerializeField]
    private RecursiveEnabledChoice _current;
    [ContextMenu(nameof(GenerateTree), false)]
    public void GenerateTree()
    {
        _current = new RecursiveEnabledChoice(_rootChoice, new HashSet<Choice>());
    }



    /*private VisualNovelManager _manager;
    private void Awake()
    {
        _manager = GameObject.Instantiate<VisualNovelManager>(null);
        _manager.enabled = false;
    }*/
    public void ProcessSequence(int branchIndex)
    {
        if (branchIndex < 0 || branchIndex > _current._subChoices.Count || !_current._subChoices[branchIndex].enabled)
            Debug.LogError("Wrong path, existing ? : " + !(branchIndex < 0 || branchIndex > _current._subChoices.Count)
                + " choice available/enabled in current mission ? : " + _current._subChoices[branchIndex].enabled
                + " Expect weird things to happen from now, good luck my friends...");
        _current = _current._subChoices[branchIndex];
    }

    internal void OnValidate()
    {
        Debug.LogWarning("Clear list of sons when disabled");
    }

    public bool IsFinalNode
    {
        get
        {
            return _current._subChoices.Count == 0 || (_current._subChoices.Count == 1 && _current._subChoices[0].choice is FinalChoice);
        }
    }

}