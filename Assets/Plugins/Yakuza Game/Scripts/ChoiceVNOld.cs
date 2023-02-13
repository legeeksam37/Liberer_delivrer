using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable,Obsolete()]
public class ChoiceVNOld
{
    public string textChoiceKey;
    //Trigger a specific sequence after selecting 
    public VNSequence postChoiceSequence;

    //TODO Create bonus/malus system 
    public List<UnityEvent> changeList = new List<UnityEvent>();
    //each one of them will have an attributed fonction that will change VariableConfig in GM
    //Maybe use UnityEvents
}

