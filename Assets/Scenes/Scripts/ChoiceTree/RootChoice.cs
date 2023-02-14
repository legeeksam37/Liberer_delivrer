using ScenarioStructures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootChoice : MonoBehaviour
{
    public Mission _mission;
    [ContextMenu(nameof(UpdateLeaves), false)]
    public void UpdateLeaves()
    {
        _mission.UpdateLeaves();
    }
}
