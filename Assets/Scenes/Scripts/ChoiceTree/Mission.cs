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
    private RecursiveEnabledChoice _tree;
    private RecursiveEnabledChoice _current;
    public string CurrentName => _current.choice.name;
    [ContextMenu(nameof(GenerateTree), false)]
    public void GenerateTree()
    {
        _tree = new RecursiveEnabledChoice(_rootChoice, new HashSet<Node>());
        Init();
    }
    public void Init()
    {
        _current = _tree;
    }
    /// <summary>
    /// Process sequence based on absolute branche index, not considering the enabled or disabled one; you can possibly call a disabled one, but a fixed option will always have same index
    /// </summary>
    /// <param name="branchIndex"></param>
    public void ProcessSequenceAbsolute(int branchIndex)
    {
        ProcessSequence(_current._subChoices, branchIndex);
    }
    /// <summary>
    /// Process sequence based on relative branche index, considering the enabled or disabled one; you will only call a valid one, but a fixed option will always have a different index someTimes
    /// </summary>
    /// <param name="branchIndex"></param>
    public void ProcessSequenceRelative(int branchIndex)
    {
        ProcessSequence(_current._subChoices.Where(s => s.enabled).ToList(), branchIndex);
    }
    public void ProcessSequenceRandom()
    {
        List<RecursiveEnabledChoice> recursiveEnabledChoices = _current._subChoices.Where(s => s.enabled).ToList();
        ProcessSequence(recursiveEnabledChoices, UnityEngine.Random.Range(0, recursiveEnabledChoices.Count));
    }
    private void ProcessSequence(List<RecursiveEnabledChoice> choices, int branchIndex)
    {
        if (branchIndex < 0)
        {
            Debug.LogError("Negative branch index, check call");
            return;
        }
        if (branchIndex >= choices.Count)
        {
            Debug.LogError(branchIndex + "higher than list size, list being (if empty, mean we're at a leaf): " + string.Join(';', choices.Select(c => c.choice.name)));
            return;
        }
        if (!choices[branchIndex].enabled)
        {
            Debug.LogError("Branch disabled in mission definition, weird things will happen from now, we're off track");
        }
        _current = choices[branchIndex];
    }
    internal void OnValidate()
    {
        Debug.LogWarning("Clear list of sons when disabled");
    }
    public bool IsFinalNode
    {
        get
        {
            return _current._subChoices.Count == 0 || (_current._subChoices.Count == 1 && _current._subChoices[0].choice is FinalNode);
        }
    }
}