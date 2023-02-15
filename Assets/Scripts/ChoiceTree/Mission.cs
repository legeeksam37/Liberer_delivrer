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
    private BuildingType _targetedBuilding;
    public BuildingID TargetedBuilding => Quest.FindBuilding(_targetedBuilding);

    [SerializeField]
    private Sprite _logo;
    public Sprite Logo { get => _logo; }
    [SerializeField]
    private RecursiveEnabledChoice _tree;
    private RecursiveEnabledChoice _current;
    public RecursiveEnabledChoice Current => _current;
    public string CurrentName => Current.choice.name;
    [ContextMenu(nameof(GenerateTree), false)]
    public void GenerateTree()
    {
        var oldTree = _tree;
        _tree = new RecursiveEnabledChoice(_rootChoice, true);
        /*foreach(var node in oldTree)
        {
            foreach(var newNode in _tree)
            {
                node    
                if(node.choice.GetInstanceID() == newNode.choice.GetInstanceID())
                    newNode.GetEnumerator
                }
            }
        }*/
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
    public bool ProcessSequenceAbsolute(int branchIndex)
    {
        return ProcessSequence(_current._subChoices, branchIndex);
    }
    /// <summary>
    /// Process sequence based on relative branche index, considering the enabled or disabled one; you will only call a valid one, but a fixed option will always have a different index someTimes
    /// </summary>
    /// <param name="branchIndex"></param>
    public bool ProcessSequenceRelative(int branchIndex)
    {
        return ProcessSequence(_current._subChoices.Where(s => s.enabled).ToList(), branchIndex);
    }
    public bool ProcessSequenceRandom()
    {
        List<RecursiveEnabledChoice> recursiveEnabledChoices = _current._subChoices.Where(s => s.enabled).ToList();
        return ProcessSequence(recursiveEnabledChoices, UnityEngine.Random.Range(0, recursiveEnabledChoices.Count));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="choices"></param>
    /// <param name="branchIndex"></param>
    /// <returns>Globally return true if we can and should keep rolling or not.False if error or next node will be a final one or current is a final one (we shouldn't get this far)</returns>
    private bool ProcessSequence(List<RecursiveEnabledChoice> choices, int branchIndex)
    {
        if (branchIndex < 0)
        {
            Debug.LogError("Negative branch index, check call");
            return false;
        }
        if (branchIndex >= choices.Count)
        {
            Debug.LogError(branchIndex + "higher than list size, list being (if empty, mean we're at a leaf): " + string.Join(';', choices.Select(c => c.choice.name)));
            return false;
        }
        if (!choices[branchIndex].enabled)
        {
            Debug.LogError("Branch disabled in mission definition, weird things will happen from now, we're off track");
            return false;
        }
        _current = choices[branchIndex];
        Debug.Log("Mission state : " + CurrentName);
        if (!NextIsFinal)
            return true;
        else
        {
            _current = _current._subChoices[0];
            return false;
        }
    }
    public List<T> GetOptions<T>() where T : Enum
    {
        var options = new List<T>();
        int i = 0;
        foreach (var c in _current._subChoices)
            if (c.enabled)
                options.Add((T)Enum.ToObject(typeof(T), i++));
        return options;
    }
    internal void OnValidate()
    {
        //Debug.LogWarning("Clear list of sons when disabled");
    }
    public bool NextIsFinal
    {
        get
        {
            return /*_current._subChoices.Count == 0 ||*/ (_current._subChoices.Count == 1 && _current._subChoices[0].choice is FinalNode);
        }
    }

}