using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC Info", menuName = "Scriptable Objects/NPC Info")]
public class NPCInfo : ScriptableObject
{
    [SerializeField] private string NPCName;
    [Header("Dialogue")]
    [SerializeField] private List<Dialogue> list;
    public string currentDialogue;
    private int currentDialogueIndex;
    [Header("Quest")]
    [SerializeField] public bool hasQuest;

    public void ProgressCurrentDialogue()
    {
        if (currentDialogueIndex < list.Count)
        {
            currentDialogueIndex += 1;
            currentDialogue = list[currentDialogueIndex].dialogue; 
        }
    }

    public void ResetValues()
    {
        currentDialogueIndex = 0;
        currentDialogue = list[currentDialogueIndex].dialogue;
    }
}

[System.Serializable]
struct Dialogue
{
    public string dialogue;
    [SerializeField] private bool goToNextDialogue;
    [SerializeField] private bool continueDialogue;
}
