using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "VisualNovel/VNDialogue")]
public class VNDialogue : ScriptableObject 
{
    public Sprite spriteCharacter;
    public CharacterVN character;
    public string keyTextCharacter;
    public Animation animationCharacter;
}
