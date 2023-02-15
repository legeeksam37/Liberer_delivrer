using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "VisualNovel/TriggeringSequence")]
public class TriggeringSequenceVN : ScriptableObject
{
    public VNSequence sequenceToPlug;
    [SerializeField] private bool _isTriggered;
    public int importanceValue;
    public int offsetIndex;
    
    public void TriggerSequence()
    {
        if (!_isTriggered)
        {
            VisualNovelManager.Instance.PlugVNSequence(sequenceToPlug,importanceValue,offsetIndex);
        }
        _isTriggered = true;
    }

    public void ResetTrigger()
    {
        sequenceToPlug.indexDialogue = 0;
        _isTriggered = false;
    }

    public bool isTriggered()
    {
        return _isTriggered;
    }

}
