using System.Collections.Generic;
using System;
using UnityEngine.Events;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New choice", menuName = "Scenarisation/Choice")]
public class Choice : ScriptableObject
{
    //Trigger a specific sequence after selecting 
    public List<Choice> postChoiceSequence;

    //TODO Create bonus/malus system 
    public List<UnityEvent> changeList = new List<UnityEvent>();
    //each one of them will have an attributed fonction that will change VariableConfig in GM
    //Maybe use UnityEvents
}
[Serializable]
public class RecursiveEnabledChoice
{
    public Choice choice;
    public bool enabled;
    public List<RecursiveEnabledChoice> _subChoices;
    public RecursiveEnabledChoice(Choice choice)
    {
        this.choice = choice;
        this.enabled = true;
        this._subChoices = new List<RecursiveEnabledChoice>();
    }
    public RecursiveEnabledChoice(Choice choice, HashSet<Choice> visited) : this(choice)
    {
        visited.Add(choice);
        //Check if nodes is on the very bottom, aka 0 child or not
        bool leaf = true;
        foreach (var subChoice in choice.postChoiceSequence)
        {
            if (!visited.Contains(subChoice))
            {
                _subChoices.Add(new RecursiveEnabledChoice(subChoice, visited));
                leaf = false;
            }
        }
        if (leaf)
            _subChoices.Add(new RecursiveEnabledChoice(AssetDatabase.LoadAssetAtPath<FinalChoice>("Assets/Scriptables/DefaultMessage.asset")));
    }


}