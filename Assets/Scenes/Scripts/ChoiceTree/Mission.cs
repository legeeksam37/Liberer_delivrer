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
        _current = new RecursiveEnabledChoice(_rootChoice, new HashSet<Node>());
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
    private void ProcessSequence(List<RecursiveEnabledChoice> choices, int branchIndex)
    {
        if (branchIndex < 0 || branchIndex > choices.Count || !choices[branchIndex].enabled)
            Debug.LogError("Wrong path, existing ? : " + !(branchIndex < 0 || branchIndex > choices.Count)
                + " choice available/enabled in current mission ? : " + choices[branchIndex].enabled
                + " Expect weird things to happen from now, good luck my friends...");
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