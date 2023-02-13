using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VisualNovel/VNDialogueChoice")]
public class VNDialogueChoice : VNDialogue
{
    //At the end of the dialogue, a pop up will spawn with a specific choice 
    public List<ChoiceVN> choiceVNList = new List<ChoiceVN>();
}
