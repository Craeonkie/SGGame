using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC Info", menuName = "Scriptable Objects/NPC Info")]
public class NPCInfo : ScriptableObject
{
    public string NPCName;
    [Header("Dialogue")]
    [SerializeField] private List<Dialogue> initialList;
    [SerializeField] private List<Dialogue> completeQuestList;
    private int currentDialogueIndex;
    [Header("Quest")]
    public bool hasQuest;
    public Item requiredItem;
    public bool finishedQuest;
    private bool interactedWithOnce;

    public bool ProgressCurrentDialogue()
    {
        if (currentDialogueIndex < initialList.Count - 1 && !interactedWithOnce)
        {
            currentDialogueIndex += 1;
            return true;
        }
        else if (hasQuest)
        {
            if (currentDialogueIndex < completeQuestList.Count - 1 && interactedWithOnce)
            {
                currentDialogueIndex += 1;
                return true;
            }
        }
        interactedWithOnce = true;
        currentDialogueIndex = 0;
        return false;
    }

    public Dialogue ReturnCurrentDialogue()
    {
        if (hasQuest && interactedWithOnce)
        {
            return completeQuestList[currentDialogueIndex];
        }
        return initialList[currentDialogueIndex];
    }

    public void ResetValues()
    {
        currentDialogueIndex = 0;
        finishedQuest = false;
        interactedWithOnce = false;
    }
}

[System.Serializable]
public struct Dialogue
{
    public string dialogue;
    public bool isNPCSpeaking;

    public Dialogue(string temp1, bool temp2)
    {
        dialogue = temp1;
        isNPCSpeaking = temp2;
    }
}