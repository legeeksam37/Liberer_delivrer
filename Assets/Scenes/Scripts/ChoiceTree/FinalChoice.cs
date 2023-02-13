using UnityEngine;
using ScenarioStructures;
[CreateAssetMenu(fileName = "New final choice", menuName = "Scenarisation/Final Choice")]
public class FinalChoice : Choice
{
    [SerializeField]
    private string _message;
    [SerializeField]
    private Result _result;
}