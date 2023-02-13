using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VisualNovel/VNSequence")]
public class VNSequence : ScriptableObject
{
    public List<VNDialogue> dialogueList = new List<VNDialogue>();
    public int indexDialogue;
    public CharacterVN mainCharacterSequence;
    
}
