using System.Collections.Generic;
using System;
using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(fileName = "New choice", menuName = "Scenarisation/Choice")]
public class Choice : ScriptableObject
{
    public (bool, string) val;
    public string textChoiceKey;
    //Trigger a specific sequence after selecting 
    public List<EnabledChoice> postChoiceSequence;

    //TODO Create bonus/malus system 
    public List<UnityEvent> changeList = new List<UnityEvent>();
    //each one of them will have an attributed fonction that will change VariableConfig in GM
    //Maybe use UnityEvents
}
[System.Serializable]
public struct EnabledChoice
{
    public bool available;
    public Choice choice;

    public EnabledChoice(bool available, Choice choice)
    {
        this.available = available;
        this.choice = choice;
    }

    public override bool Equals(object obj)
    {
        return obj is EnabledChoice other &&
               available == other.available &&
               EqualityComparer<Choice>.Default.Equals(choice, other.choice);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(available, choice);
    }

    public void Deconstruct(out bool available, out Choice choice)
    {
        available = this.available;
        choice = this.choice;
    }

    public static implicit operator (bool available, Choice choice)(EnabledChoice value)
    {
        return (value.available, value.choice);
    }

    public static implicit operator EnabledChoice((bool available, Choice choice) value)
    {
        return new EnabledChoice(value.available, value.choice);
    }
}