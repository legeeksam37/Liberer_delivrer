using System.Collections.Generic;
using System;
using UnityEngine.Events;
using UnityEngine;
using UnityEditor;
using System.Collections;

[CreateAssetMenu(fileName = "New choice", menuName = "Scenarisation/Choice")]
public class Choice : Node
{
    //Trigger a specific sequence after selecting 
    public List<Node> postChoiceSequence = new List<Node>();

    //TODO Create bonus/malus system 
    public List<UnityEvent> changeList = new List<UnityEvent>();

    public override List<Node> PostChoiceSequence => postChoiceSequence;
    //each one of them will have an attributed fonction that will change VariableConfig in GM
    //Maybe use UnityEvents
}
public abstract class Node : ScriptableObject
{
    public abstract List<Node> PostChoiceSequence { get; }
}
[Serializable]
public class RecursiveEnabledChoice : IEnumerable<RecursiveEnabledChoice>
{
    public Node choice;
    public bool enabled;
    public List<RecursiveEnabledChoice> _subChoices;
    public RecursiveEnabledChoice(Node choice)
    {
        this.choice = choice;
        this.enabled = true;
        this._subChoices = new List<RecursiveEnabledChoice>();
    }
    public RecursiveEnabledChoice(Node choice, HashSet<Node> visited) : this(choice)
    {
        visited.Add(choice);
        //Check if nodes is on the very bottom, aka 0 child or not
        bool leaf = true;
        foreach (var subChoice in choice.PostChoiceSequence)
        {
            if (!visited.Contains(subChoice))
            {
                _subChoices.Add(new RecursiveEnabledChoice(subChoice, visited));
                leaf = false;
            }
        }
        if (leaf)
            _subChoices.Add(new RecursiveEnabledChoice(AssetDatabase.LoadAssetAtPath<FinalNode>("Assets/Scriptables/DefaultMessage.asset")));
    }

    public IEnumerator<RecursiveEnabledChoice> GetEnumeratorAll()
    {
        yield return this;
        foreach (var subChoice in _subChoices)
            yield return subChoice;
    }
    /// <summary>
    /// Leaves only
    /// </summary>
    /// <returns></returns>
    public IEnumerator<RecursiveEnabledChoice> GetEnumerator()
    {
        if (_subChoices.Count == 0)
            yield return this;
        else
            foreach (var subChoice in _subChoices)
                yield return subChoice;
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        yield return GetEnumerator();
    }
}