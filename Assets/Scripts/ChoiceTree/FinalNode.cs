using UnityEngine;
using ScenarioStructures;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New final choice", menuName = "Scenarisation/Final Choice")]
public class FinalNode : Node
{
    [SerializeField]
    private string _message;
    [SerializeField]
    private Result _result;

    public override List<Node> PostChoiceSequence => null;
}